using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_Modiriat_Daneshgah_TeamKhodemon
{
    public static class Class1
    {
    /// ///////////////////////////////////////////////////////////////////////////////modir

        public static int IndexOfModir;
        public static string Modir_Fname;
        public static string Modir_Lname;
        public static string Modir_FatherName;
        public static string Modir_Madrak;
        public static decimal Modir_NationalCode;
        public static decimal Modir_Phone;
        public static decimal Modir_Code;


        public static int IndexOfTeacherSelect;
        public static decimal OstadSelectCode;
        public static decimal OstadSelecetPassword;
        public static string OstadSelectFName;
        public static string OstadSelectLName;


        /////////Student
        public static int IndexOfStudentSelect;
        public static decimal Student_Id;
        public static string Student_FName;
        public static string Student_LName;
        public static string Student_FatherName;
        public static decimal Student_NationalCode;
        public static string Student_BirtDate;
        public static string Student_Reshte;
        public static string Student_Maghta;
        public static int Student_Shahriye;
        public static int Student_Numberterm;
        public static string Student_Address;
        public static decimal Student_Phone;
        public static decimal Student_Password;


        public static Form_Modir_Ostad F_Ostad = new Form_Modir_Ostad();
        //.Form_Modir_Ostad_Load(sender, e);


        //////Lesson_Select
        public static int RowIndex_Lesson;
        public static string LessonName;
        public static int LessonCode;
        public static int EraeeCode;
        public static decimal TeacherCode;
        public static string TeacherName;
        public static int NumberVahed;
        public static string LessonTime;
        public static string LessonDay;
        public static string ExamDate;
        public static string ExamTime;
        public static int TakenForTeacher;



        ////////////////////////////////////////////////////////////////////////////////Ostad

        public static int TeachIndex;
        public static string TeachName;
        public static string TeachLastName;
        public static string TeachFather;
        public static string TeachMadrak;
        public static int TeachNationalCode;
        public static decimal TeachPhone;
        public static int TeachCode;

        /////////////////////////////////////////////////////////////////////////////Student

        public static int IndexOfStudent;
        public static string Student_FirstName;
        public static string Student_LastName;
        public static int Student_Code;
        public static string Student_EnterDate;
        public static string Student_FathersName;
        public static int StudentNationalCode;//Student_NationalCode
        public static string Student_Reshteh;
        public static string Student_DateOfBirth;
        public static string StudentMaghta;//Student_Maghta
        public static int Student_Term;

        public static int StudentShahriye;//khodam Ezafe kardam
        public static int StudentShahriye_Koll;//khodam Ezafe kardam

        public static int IndexOfStudentSelect_formStudent;// IndexOfStudentSelect
        public static int StudentSelectCode;
        public static int StudentSelecetPassword;
    }
}
