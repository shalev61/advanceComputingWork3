//Shalev Goldshtein 212210280
using System;
using System.Collections.Generic;

using System.Text;

using System.Linq;

using System.Threading.Tasks;

namespace Excercise_1
{
    public class SingleMission : IMission
    {
        //on event that calc
        public event EventHandler<double> OnCalculate;


        //the func to do
        private Func<double, double> someFunc;

        //the members that in the interface
        public string Name { get; }
        public string Type { get; }



        /*
         * the constarctor of the single mission
         */
        public SingleMission(Func<double, double> func, string missionName)
        {
            //intilize parmters in c#
            this.someFunc = func;

            this.Name = missionName;

            //because we in the single mission
            this.Type = "Single";

        }


        /* 
         * the clc func we get some value and clc with the func
         */
        public double Calculate(double val)
        {
            //temporery member for doesn't get hurt the value
            double temp = this.someFunc(val);
            double x = temp;
            //in this func
            OnCalculate?.Invoke(this, temp);
            return x;
        }
    }
}
