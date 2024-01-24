using System;
class testa
{
    private string m_name;
    private int m_age;
    public testa()
    {

    }

    public void setname(string name)
    {
        this.m_name = name;        
    }

    public void show()
    {
        Console.WriteLine(String.Format("name = {0} age = {1}",m_name,m_age));
    }
}