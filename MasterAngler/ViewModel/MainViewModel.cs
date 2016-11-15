using System;
using System.Reflection;
using GalaSoft.MvvmLight;
using MasterAngler.Wow.Patch;

namespace MasterAngler.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// See http://www.mvvmlight.net
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase {
        public MainViewModel() {

            //Console.WriteLine("==========");
            //Console.WriteLine("Object");

            //FieldInfo[] fi = typeof(Descriptors.ObjectFields).GetFields();
            //Console.WriteLine(fi.Length);
            //foreach (FieldInfo info in fi)
            //{
            //    Console.WriteLine($"{info.GetValue(info)} {info.Name}");
            //}

            //Console.WriteLine("==========");
            //Console.WriteLine("UNITS");

            //fi = typeof(Descriptors.UnitFields).GetFields();
            //Console.WriteLine(fi.Length);
            //foreach (FieldInfo info in fi)
            //{
            //    Console.WriteLine($"{info.GetValue(info)} {info.Name}");
            //}

            //Console.WriteLine("==========");
            //Console.WriteLine("Player");

            //fi = typeof(Descriptors.PlayerFields).GetFields();
            //Console.WriteLine(fi.Length);
            //foreach (FieldInfo info in fi)
            //{
            //    Console.WriteLine($"{info.GetValue(info)} {info.Name}");
            //}
        }
    }
}