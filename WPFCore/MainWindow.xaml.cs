using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFCore
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Student stud = new Student();
        public MainWindow()
        {
            InitializeComponent();
            PopulateStudentData();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var studName = stuName.Text;
            var studAddress = stuAddress.Text;
            using (AppDbContext db = new AppDbContext())
            {
                Student student = new Student()
                {
                    Name = studName,
                    Address = studAddress
                };
                db.Students.Add(student);
                db.SaveChanges();
                StudentList.Items.Clear();
                PopulateStudentData();
            }
        }

        private void PopulateStudentData()
        {
            using (AppDbContext db = new AppDbContext())
            {
                var studentData = db.Students.ToList();
                foreach (var item in studentData)
                {
                    StudentList.Items.Add(item);
                }
            }
        }

        private void StudentList_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var item = (ListView)sender;
            var student = (Student)item.SelectedItem;
            //MessageBox.Show(student.Address);
            if (DeleteCheck.IsChecked == true)
            {
                var studentId = student.Id;
                using (AppDbContext db = new AppDbContext())
                {
                    var findStud = db.Students.Find(studentId);
                    db.Students.Remove(findStud);
                    db.SaveChanges();
                    StudentList.Items.Clear();
                    PopulateStudentData();
                }
            }
            else if (EditCheck.IsChecked == true)
            {
                var studentId = student.Id;
                using (AppDbContext db = new AppDbContext()) {
                    var findStud = db.Students.Find(studentId);
                    stuId.Text = findStud.Id.ToString();
                    stuName.Text = findStud.Name;
                    stuAddress.Text = findStud.Address;
                };
            }
            else if(ClearCheck.IsChecked == true)
            {
                stuId.Text = "";
                stuName.Text = "";
                stuAddress.Text = "";
            }
            else
            {
                MessageBox.Show($"Student Name is {student.Name} and his address is {student.Address}");
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var studID = Convert.ToInt32(stuId.Text);
            using (AppDbContext db = new AppDbContext())
            {
                var studentID = db.Students.Find(studID);
                studentID.Name = stuName.Text;
                studentID.Address = stuAddress.Text;
                db.Students.Update(studentID);
                db.SaveChanges();
                StudentList.Items.Clear();
                PopulateStudentData();
            }
        }

        
    }
}
