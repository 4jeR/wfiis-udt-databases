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
public struct Pen : INullable, IBinarySerialize
{
    private double m_Price;
    private string m_Color;
    private int m_Length;
    private bool m_Clickable;
    private bool m_Null;

    public Pen(double price, string color, int len, bool clickable)
    {
        m_Price = price;
        m_Color = color;
        m_Length = len;
        m_Clickable = clickable;
        m_Null = false;
    }

    public string GetName() { return "Pen"; }
    public double GetPrice() { return m_Price; }
    public string GetColor() { return m_Color; }
    public int GetLength() { return m_Length; }
    public bool GetClickable() { return m_Clickable; }


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

            br.Append("clickable: ");

            if (m_Clickable)
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
            return m_Null;
        }
    }

    public static Pen Null
    {
        get
        {
            Pen h = new Pen();
            h.m_Null = true;
            return h;
        }
    }





    [SqlMethod(OnNullCall = false)]
    public static Pen Parse(SqlString s)
    {
        if (s.IsNull) return Null;
        else
        {
            string[] values = s.Value.Split(@"/".ToCharArray());

            Pen pen = new Pen();

            pen.m_Price = Double.Parse(values[0]);
            pen.m_Color = values[1];
            pen.m_Length = int.Parse(values[2]);
            pen.m_Clickable = values[3].Equals("yes");




            if (!pen.Validate()) throw new ArgumentException("Invalid values!");
            return pen;
        }
    }



    private bool Validate()
    {
        return ((0 < m_Price) && (6 < m_Length));
    }




    public void Write(System.IO.BinaryWriter w)
    {
        int maxStringSize = 20;
        string paddedString;
        w.Write(m_Price);
        paddedString = m_Color.PadRight(maxStringSize, '\0');
        for (int i = 0; i < paddedString.Length; i++)
        {
            w.Write(paddedString[i]);
        }
        w.Write(m_Length);
        w.Write(m_Clickable);
    }


    public void Read(System.IO.BinaryReader r)
    {
        int maxStringSize = 20;
        char[] chars;
        int stringEnd;
        string stringValue;

        m_Price = r.ReadDouble();
        chars = r.ReadChars(maxStringSize);
        stringEnd = Array.IndexOf(chars, '\0');
        if (stringEnd == 0)
        {
            stringValue = null;
            return;
        }
        stringValue = new String(chars, 0, stringEnd);
        m_Color = stringValue;
        m_Length = r.ReadInt32();
        m_Clickable = r.ReadBoolean();
    }
}


