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
public struct Glue : INullable, IBinarySerialize
{
    private double m_Price;
    private int m_Radius;
    private int m_Height;
    private bool m_Null;


    public Glue(double price, int radius, int height)
    {
        m_Price = price;
        m_Radius = radius;
        m_Height = height;
        m_Null = false;
    }

    public string GetName() { return "Glue"; }
    public double GetPrice() { return m_Price; }
    public int GetRadius() { return m_Radius; }
    public int GetHeight() { return m_Height; }



    public override string ToString()
    {
        if (this.IsNull) return "NULL";
        else
        {
            StringBuilder br = new StringBuilder(GetName() + "-> ");
            br.Append("Price: ");
            br.Append(m_Price + " PLN, ");

            br.Append("cylinder size: radius: ");
            br.Append(m_Radius + "cm, height: ");
            br.Append(m_Height + "cm");
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

    public static Glue Null
    {
        get
        {
            Glue h = new Glue();
            h.m_Null = true;
            return h;
        }
    }
    [SqlMethod(OnNullCall = false)]
    public static Glue Parse(SqlString s)
    {
        if (s.IsNull)
            return Null;

        else
        {
            string[] values = s.Value.Split(@"/".ToCharArray());

            Glue glue = new Glue();

            glue.m_Price = Double.Parse(values[0]);
            glue.m_Radius = int.Parse(values[1]);
            glue.m_Height = int.Parse(values[2]);



            if (!glue.Validate()) throw new ArgumentException("Invalid values!");
            return glue;
        }
    }


    private bool Validate()
    {
        return ((0 < m_Price) && (0 < m_Radius && m_Radius <= 5) && (5 <= m_Height && m_Height <= 15));
    }

    public void Write(System.IO.BinaryWriter w)
    {
        w.Write(m_Price);
        w.Write(m_Radius);
        w.Write(m_Height);
    }

    public void Read(System.IO.BinaryReader r)
    {
        m_Price = r.ReadDouble();
        m_Radius = r.ReadInt32();
        m_Height = r.ReadInt32();
    }
}


