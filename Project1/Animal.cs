using System;
using System.Reflection;
using System.Xml;
using System.IO;

namespace Project1
{
    abstract class Animal
    {
        public abstract int Legs();

        public abstract string MakeNoise();

        public virtual string ToXML()
        {
            Assembly ex = Assembly.GetExecutingAssembly();

            using (var stringWriter = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(stringWriter))
                {
                    writer.WriteStartDocument();
                    writer.WriteStartElement("Classes");

                    //store classes, but not main program class
                    Type[] types = ex.GetTypes();
                    foreach (var t in types)
                    {
                        if (t.Name == "Program")
                        {
                            continue;
                        }
                        else
                        {
                            writer.WriteStartElement("Namespace", t.Namespace);
                            writer.WriteEndElement();
                            writer.WriteStartElement("Basetype", t.BaseType.ToString());
                            writer.WriteEndElement();
                            writer.WriteStartElement("Class", t.Name);

                            //store methods, but not object methods
                            MethodInfo[] methods = t.GetMethods();
                            foreach (var m in methods)
                            {
                                if (m.Name == "GetHashCode" || m.Name == "Equals" || m.Name == "ToString")
                                {
                                    continue;
                                }
                                else
                                {
                                    writer.WriteElementString("Method", m.Name);
                                    ParameterInfo[] param = m.GetParameters();
                                    foreach (var p in param)
                                    {
                                        writer.WriteElementString("Parameter : {0} Type : {1}", p.Name, p.ParameterType.ToString());
                                    }
                                }
                            }
                        }
                        writer.WriteEndElement();
                    }
                    writer.WriteEndDocument();
                }
                return stringWriter.ToString();
            }
        }
    }

    class Dog : Animal
    {
        public override int Legs()
        {
            return 4;
        }

        public override string MakeNoise()
        {
            return "Woof";
        }
        public override string ToXML()
        {
            return base.ToXML();
        }
    }

    class Duck: Animal
    {
        public override int Legs()
        {
            return 2;
        }

        public override string MakeNoise()
        {
            return "Quack";
        }

        public override string ToXML()
        {
            return base.ToXML();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Dog d = new Dog();
            //d.ToXML();
            Console.WriteLine(d.ToXML());
        }
    }
}
