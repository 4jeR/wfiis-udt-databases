using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

[Serializable]
[Microsoft.SqlServer.Server.SqlUserDefinedType(
    Format.UserDefined,
    MaxByteSize = 8000,
    IsByteOrdered = true,
    ValidationMethodName = "Validate"
 )]
public struct Notebook : INullable, IBinarySerialize
{
    private double m_Price;
    private int m_PagesCount;
    private string m_Type;
    private string m_PageSize;
    private bool m_HardCover;
    private bool m_Null;

    public Notebook(double price, int pagesCount, string type, string pageSize, bool hardCover)
    {
        m_Price = price;
        m_PagesCount = pagesCount;
        m_Type = type;
        m_PageSize = pageSize;
        m_HardCover = hardCover;
        m_Null = false;
    }

    public string GetName() { return "Notebook"; }
    public double GetPrice() { return m_Price; }
    public int GetPagesCount() { return m_PagesCount; }
    public string GetTypeOf() { return m_Type; }
    public string GetPageSize() { return m_PageSize; }
    public bool GetHardCover() { return m_HardCover; }

    public override string ToString()
    {
        if (this.IsNull) return "NULL";
        else
        {
            StringBuilder br = new StringBuilder(GetName() + "-> ");
            br.Append("Price: ");
            br.Append(m_Price + " PLN, ");
            br.Append(m_PagesCount + " pages, ");

            br.Append("type: ");
            br.Append(m_Type + " ");
            br.Append(m_PageSize + ", hard covered: ");
            if (m_HardCover)
                br.Append("yes");
            else
                br.Append("no");


            return br.ToString();
        }
    }

    public bool IsNull
    {
        get
        {
            // Put your code here
            return m_Null;
        }
    }

    public static Notebook Null
    {
        get
        {
            Notebook h = new Notebook();
            h.m_Null = true;
            return h;
        }
    }

    [SqlMethod(OnNullCall = false)]
    public static Notebook Parse(SqlString s)
    {
        if (s.IsNull) return Null;
        else
        {
            string[] values = s.Value.Split(@"/".ToCharArray());

            Notebook nb = new Notebook();

            nb.m_Price = Double.Parse(values[0]);
            nb.m_PagesCount = int.Parse(values[1]);
            nb.m_Type = values[2];
            nb.m_PageSize = values[3];
            nb.m_HardCover = values[4].Equals("yes");

            if (!nb.Validate()) throw new ArgumentException("Invalid values!");
            return nb;
        }
    }

    private bool Validate()
    {
        return ((0 < m_Price)
            && (16 <= m_PagesCount && m_PagesCount <= 160)
            && (m_Type.Equals("in lines") || m_Type.Equals("checkered") || m_Type.Equals("clear"))
            && (m_PageSize.Contains("A") || m_PageSize.Contains("B") || m_PageSize.Contains("C"))
        );
    }


    public void Write(System.IO.BinaryWriter w)
    {
        int maxStringSize = 30;
        string paddedString;
        w.Write(m_Price);
        w.Write(m_PagesCount);
        paddedString = m_Type.PadRight(maxStringSize, '\0');
        for (int i = 0; i < paddedString.Length; i++)
        {
            w.Write(paddedString[i]);
        }
        paddedString = m_PageSize.PadRight(maxStringSize, '\0');
        for (int i = 0; i < paddedString.Length; i++)
        {
            w.Write(paddedString[i]);
        }
        w.Write(m_HardCover);
    }

    public void Read(System.IO.BinaryReader r)
    {
        int maxStringSize = 30;
        char[] chars;
        int stringEnd;
        string stringValue;

        m_Price = r.ReadDouble();
        m_PagesCount = r.ReadInt32();

        // read type of notebook
        chars = r.ReadChars(maxStringSize);
        stringEnd = Array.IndexOf(chars, '\0');
        if (stringEnd == 0)
        {
            stringValue = null;
            return;
        }
        stringValue = new String(chars, 0, stringEnd);
        m_Type = stringValue;

        // read page size, ex: B5, A4 etc.
        chars = r.ReadChars(maxStringSize);
        stringEnd = Array.IndexOf(chars, '\0');
        if (stringEnd == 0)
        {
            stringValue = null;
            return;
        }
        stringValue = new String(chars, 0, stringEnd);
        m_PageSize = stringValue;
        m_HardCover = r.ReadBoolean();
    }



}


