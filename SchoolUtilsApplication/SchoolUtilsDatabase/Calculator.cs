using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;
using System.Collections.Generic;


[Serializable]
[Microsoft.SqlServer.Server.SqlUserDefinedType(
Format.UserDefined,
MaxByteSize = 8000,
IsByteOrdered = true,
ValidationMethodName = "Validate"
)]
public struct Calculator : INullable, IBinarySerialize
{
    private double m_Price;
    private string m_Brand;
    private int m_Width;
    private int m_Height;
    private int m_DigitsDisplayed;
    private bool m_Null;


    public Calculator(double price, string brand, int width, int height, int digits_displayed)
    {
        m_Price = price;
        m_Brand = brand;
        m_Width = width;
        m_Height = height;
        m_DigitsDisplayed = digits_displayed;
        m_Null = false;
    }

    public string GetName() { return "Calculator"; }

    public double GetPrice() { return m_Price; }
    public string GetBrand() { return m_Brand; }
    public int GetWidth() { return m_Width; }
    public int GetHeight() { return m_Height; }
    public int GetDigits() { return m_DigitsDisplayed; }

    public override string ToString()
    {
        if (this.IsNull) return "NULL";
        else
        {
            StringBuilder br = new StringBuilder(GetName() + "-> ");

            br.Append("Price: ");
            br.Append(m_Price + " PLN, ");

            br.Append("brand: ");
            br.Append(m_Brand + ", ");

            br.Append("Size: ");
            br.Append(m_Width + "cm x ");
            br.Append(m_Height + "cm, ");

            br.Append("digits displayed: ");
            br.Append(m_DigitsDisplayed);
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

    public static Calculator Null
    {
        get
        {
            Calculator h = new Calculator();
            h.m_Null = true;
            return h;
        }
    }

    [SqlMethod(OnNullCall = false)]
    public static Calculator Parse(SqlString s)
    {
        if (s.IsNull) return Null;
        else
        {
            string[] values = s.Value.Split(@"/".ToCharArray());

            Calculator cltr = new Calculator();

            cltr.m_Price = Double.Parse(values[0]);
            cltr.m_Brand = values[1];
            cltr.m_Width = int.Parse(values[2]);
            cltr.m_Height = int.Parse(values[3]);
            cltr.m_DigitsDisplayed = int.Parse(values[4]);



            if (!cltr.Validate()) throw new ArgumentException("Invalid values!");
            return cltr;
        }
    }


    private bool Validate()
    {

        return (
            (0 < m_Price) && (0 < m_Width) && (0 < m_Height) && (6 <= m_DigitsDisplayed)
        );
    }

    public void Write(System.IO.BinaryWriter w)
    {
        int maxStringSize = 20;
        string paddedString;
        w.Write(m_Price);
        paddedString = m_Brand.PadRight(maxStringSize, '\0');
        for (int i = 0; i < paddedString.Length; i++)
        {
            w.Write(paddedString[i]);
        }
        w.Write(m_Width);
        w.Write(m_Height);
        w.Write(m_DigitsDisplayed);
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
        m_Brand = stringValue;
        m_Width = r.ReadInt32();
        m_Height = r.ReadInt32();
        m_DigitsDisplayed = r.ReadInt32();
    }




}


