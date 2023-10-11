using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace WindowsFormsApp2
{
    
    public partial class Form1 : Form
    {//constants
        string startDatestr = "2023-03-30"; //constant date of appointment (link to database)
        string endDatestr = "2024-03-29"; //End of contract (link to database)
        int dayCounter = 0;
        int dayOffcounter = 5;// link to database

       
        public Form1()
        {
            InitializeComponent();
            this.MaximizeBox= false;
        }
        static int daysOff(DateTime currentDate, DateTime startDate, int dayOffcounter)
        {
            int dayCounter;
           
            TimeSpan difference = currentDate - startDate; //check the difference between dates
            int daysDifference = difference.Days; // minus 2 because we are counting week days not weekends
            dayCounter = (daysDifference - 2) / 17;

            if (daysDifference == 17)
            {
                dayCounter++; // for every 17 days you get one day off
            }

            dayCounter -= dayOffcounter;//  subtract from the days you already took
            if (dayCounter < 0)
            {
                dayCounter = 0; // if the day counter is less than 0 for readability it will be 0

            }
            return dayCounter;

        }


        private void Form1_Load(object sender, EventArgs e)
        {
            DateTime startDate = Convert.ToDateTime(startDatestr);
            DateTime endDate = Convert.ToDateTime(endDatestr);
            DateTime currentDate = DateTime.Now; // set

            dayCounter = daysOff(currentDate, startDate, dayOffcounter);

           lblCounter.Text = dayCounter.ToString();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dayCounter <= 0)
            {
                MessageBox.Show("You don't have enough days of\nYour Total is :" + dayCounter);
            }
            else
            {
                //take input
                int takeDayoff = Convert.ToInt32(Interaction.InputBox("How many day?","InputBox"));

                //now I gotta verify it 
                if (takeDayoff > dayCounter)
                {
                    MessageBox.Show("you do not have enough days off Choose lesser days");
                }
                else
                {
                    dayCounter -= takeDayoff; // subtract from the accumulated day off
                    dayOffcounter += takeDayoff; // add to the day off counter
                    MessageBox.Show("Great you have taken " + takeDayoff + " day off\nyou now have " + dayCounter + " available day off left");
                    lblCounter.Text = dayCounter.ToString();   
                }
            }

        }
        static int FutureDaysOff(DateTime currentDate, DateTime endDate)
        {
            TimeSpan futureDiff = endDate - currentDate;
            int futureDays = futureDiff.Days;
            int futureDayCounter = futureDays / 17;

            if (futureDays == 17)// for every 17 days that pass you get 1 day off
            {
                futureDayCounter++;
            }

            return futureDayCounter;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            DateTime endDate = Convert.ToDateTime(endDatestr);
            DateTime currentDate = DateTime.Now; // set
            DateTime startDate = Convert.ToDateTime(startDatestr);
            int futureDays = FutureDaysOff(currentDate, endDate);

            if(button2.Text == "show future days offs")
            {
                lblCounter.Text = futureDays.ToString();
                button2.Text = "show Available day offs";
                label2.Text = "Future Days off from now";
            }
            else 
            {
                lblCounter.Text = daysOff(currentDate,startDate,dayOffcounter).ToString();
                button2.Text = "show future days offs";
                label2.Text = "Available Days off";
            }

            
        }
    }
}
