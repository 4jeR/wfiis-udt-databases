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
public struct Rubber : INullable, IBinarySerialize
{
    private double m_Price;
    private int m_Width;
    private int m_Thickness;
    private int m_Height;
    private bool m_Null;

    public Rubber(double price, int width, int thick, int height)
    {
        m_Price = price;
        m_Width = width;
        m_Thickness = thick;
        m_Height = height;
        m_Null = false;
    }

    public string GetName() { return "Rubber"; }
    public double GetPrice() { return m_Price; }
    public int GetWidth() { return m_Width; }
    public int GetThickness() { return m_Thickness; }
    public int GetHeight() { return m_Height; }

    public override string ToString()
    {
        if (this.IsNull) return "NULL";
        else
        {
            StringBuilder br = new StringBuilder(GetName() + "-> ");
            br.Append("Price: ");
            br.Append(m_Price + " PLN, ");

            br.Append("Size: ");
            br.Append(m_Width + " x ");
            br.Append(m_Thickness + " x ");
            br.Append(m_Height + " cm");

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

    public static Rubber Null
    {
        get
        {
            Rubber h = new Rubber();
            h.m_Null = true;
            return h;
        }
    }

    [SqlMethod(OnNullCall = false)]
    public static Rubber Parse(SqlString s)
    {
        if (s.IsNull) return Null;
        else
        {
            string[] values = s.Value.Split(@"/".ToCharArray());

            Rubber rub = new Rubber();

            rub.m_Price = double.Parse(values[0]);
            rub.m_Width = int.Parse(values[1]);
            rub.m_Thickness = int.Parse(values[2]);
            rub.m_Height = int.Parse(values[3]);


            if (!rub.Validate()) throw new ArgumentException("Invalid values!");
            return rub;
        }
    }

    private bool Validate()
    {
        return ((0 < m_Price) && (0 < m_Width) && (0 < m_Thickness) && (0 < m_Height));
    }




    public void Write(System.IO.BinaryWriter w)
    {
        w.Write(m_Price);
        w.Write(m_Width);
        w.Write(m_Thickness);
        w.Write(m_Height);
    }


    public void Read(System.IO.BinaryReader r)
    {
        m_Price = r.ReadDouble();
        m_Width = r.ReadInt32();
        m_Thickness = r.ReadInt32();
        m_Height = r.ReadInt32();
    }
}


