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
public struct Ruler : INullable, IBinarySerialize
{

    private double m_Price;
    private int m_Length;
    private string m_Color;
    private double m_Scale;
    private bool m_Null;

    public Ruler(double price, int len, string color, double scale)
    {
        m_Price = price;
        m_Length = len;
        m_Color = color;
        m_Scale = scale;
        m_Null = false;
    }

    public string GetName() { return "Ruler"; }
    public double GetPrice() { return m_Price; }
    public int GetLength() { return m_Length; }
    public string GetColor() { return m_Color; }
    public double GetScale() { return m_Scale; }

    public override string ToString()
    {
        if (this.IsNull) return "NULL";
        else
        {
            StringBuilder br = new StringBuilder(GetName() + "-> ");
            br.Append("Price: ");
            br.Append(m_Price + " PLN, ");
            br.Append("length: ");
            br.Append(m_Length + "cm, ");
            br.Append("color: ");
            br.Append(m_Color + ", ");
            br.Append("scale: ");
            br.Append(m_Scale + "cm ");




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

    public static Ruler Null
    {
        get
        {
            Ruler h = new Ruler();
            h.m_Null = true;
            return h;
        }
    }

    [SqlMethod(OnNullCall = false)]
    public static Ruler Parse(SqlString s)
    {
        if (s.IsNull) return Null;
        else
        {
            string[] values = s.Value.Split(@"/".ToCharArray());

            Ruler rub = new Ruler();

            rub.m_Price = double.Parse(values[0]);
            rub.m_Length = int.Parse(values[1]);
            rub.m_Color = values[2];
            rub.m_Scale = double.Parse(values[3]);

            if (!rub.Validate()) throw new ArgumentException("Invalid values!");
            return rub;
        }
    }

    private bool Validate()
    {
        return (m_Price > 0 && (10 <= m_Length && m_Length <= 100) && (0.05 <= m_Scale && m_Scale <= 1));
    }


    public void Write(System.IO.BinaryWriter w)
    {
        int maxStringSize = 30;
        string paddedString;
        w.Write(m_Price);
        w.Write(m_Length);
        paddedString = m_Color.PadRight(maxStringSize, '\0');
        for (int i = 0; i < paddedString.Length; i++)
        {
            w.Write(paddedString[i]);
        }
        w.Write(m_Scale);
    }


    public void Read(System.IO.BinaryReader r)
    {
        int maxStringSize = 30;
        char[] chars;
        int stringEnd;
        string stringValue;

        m_Price = r.ReadDouble();
        m_Length = r.ReadInt32();
        chars = r.ReadChars(maxStringSize);
        stringEnd = Array.IndexOf(chars, '\0');
        if (stringEnd == 0)
        {
            stringValue = null;
            return;
        }
        stringValue = new String(chars, 0, stringEnd);
        m_Color = stringValue;
        m_Scale = r.ReadDouble();
    }
}


