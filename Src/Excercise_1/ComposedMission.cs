//Shalev Goldshtein 212210280
using System;

using System.Threading.Tasks;
using System.Linq;

using System.Text;

using System.Collections.Generic;

namespace Excercise_1
{
    public class ComposedMission : IMission
    {

        private Func<double, double> someFunc;


        //the members that from the interface
        public string Type { get; }
        public string Name { get; }

        //the event for the calculate
        public event EventHandler<double> OnCalculate;

        /*
         * clac thee mission
         */
        public double Calculate(double calcVal)
        {
            //temp for not working on the this
            double temp = this.someFunc(calcVal);

            double x = temp;
            //in this func
            OnCalculate?.Invoke(this, x);

            //return the calc
            return temp;
        }

        /*
         * constactor with only string
         */
        public ComposedMission(string missionToDo)
        {
            //the member of the class
            this.someFunc = val => val;

            this.Type = "Composed";

            this.Name = missionToDo;

        }


        /*
         * add another mission to the composted
         */
        public ComposedMission Add(Func<double, double> aFunc)
        {
            //for not working the thing
            Func<double, double> temp1 = this.someFunc;

            Func<double, double> temporery = new Func<double, double>(temp1);

            //get the val func
            this.someFunc = val => aFunc(temporery(val));

            //return the statment
            return this;
        }


        /*
         *  the constractor of the class
         */
        private ComposedMission(Func<double, double> aFunc, string missionToDo)
        {
            //the mmbers of the class
            this.someFunc = aFunc;

            //for the type of the class
            this.Type = "Composed";

            this.Name = missionToDo;


        }


    }
}