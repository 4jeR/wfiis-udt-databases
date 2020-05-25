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
public struct Scissors : INullable, IBinarySerialize
{
    private double m_Price;
    private string m_Color;
    private string m_Quality;
    private bool m_Null;

    public Scissors(double price, string color, string quality)
    {
        m_Price = price;
        m_Color = color;
        m_Quality = quality;
        m_Null = false;
    }

    public string GetName() { return "Scissors"; }
    public double GetPrice() { return m_Price; }
    public string GetColor() { return m_Color; }
    public string GetQuality() { return m_Quality; }


    public override string ToString()
    {
        if (this.IsNull) return "NULL";
        else
        {
            StringBuilder br = new StringBuilder(GetName() + "-> ");
            br.Append("Price: ");
            br.Append(m_Price + " PLN, ");
            br.Append("color: ");
            br.Append(m_Color + ", ");
            br.Append("quality: ");
            br.Append(m_Quality);


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

    public static Scissors Null
    {
        get
        {
            Scissors h = new Scissors();
            h.m_Null = true;
            return h;
        }
    }

    [SqlMethod(OnNullCall = false)]
    public static Scissors Parse(SqlString s)
    {
        if (s.IsNull) return Null;
        else
        {
            string[] values = s.Value.Split(@"/".ToCharArray());

            Scissors sciss = new Scissors();

            sciss.m_Price = double.Parse(values[0]);
            sciss.m_Color = values[1];
            sciss.m_Quality = values[2];


            if (!sciss.Validate()) throw new ArgumentException("Invalid values!");
            return sciss;
        }
    }

    private bool Validate()
    {
        return ((0 < m_Price) &&
            (m_Quality.Equals("medium")
            || m_Quality.Equals("good")
            || m_Quality.Equals("amazing")
            || m_Quality.Equals("excellent")));
    }




    public void Write(System.IO.BinaryWriter w)
    {
        int maxStringSize = 30;
        string paddedString;
        w.Write(m_Price);
        paddedString = m_Color.PadRight(maxStringSize, '\0');
        for (int i = 0; i < paddedString.Length; i++)
        {
            w.Write(paddedString[i]);
        }
        paddedString = m_Quality.PadRight(maxStringSize, '\0');
        for (int i = 0; i < paddedString.Length; i++)
        {
            w.Write(paddedString[i]);
        }

    }


    public void Read(System.IO.BinaryReader r)
    {
        int maxStringSize = 30;
        char[] chars;
        int stringEnd;
        string stringValue;

        m_Price = r.ReadDouble();

        // read color
        chars = r.ReadChars(maxStringSize);
        stringEnd = Array.IndexOf(chars, '\0');
        if (stringEnd == 0)
        {
            stringValue = null;
            return;
        }
        stringValue = new String(chars, 0, stringEnd);
        m_Color = stringValue;

        // read quality
        chars = r.ReadChars(maxStringSize);
        stringEnd = Array.IndexOf(chars, '\0');
        if (stringEnd == 0)
        {
            stringValue = null;
            return;
        }
        stringValue = new String(chars, 0, stringEnd);
        m_Quality = stringValue;

    }
}


