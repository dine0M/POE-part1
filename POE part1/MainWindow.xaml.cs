using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;



namespace POE_part1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<LecturerClaim> Claims { get; set; }
        private string uploadedFilePath = "";
        public MainWindow()
        {
            InitializeComponent();
            Claims = new ObservableCollection<LecturerClaim>();
            LecturerClaimsDataGrid.ItemsSource = Claims;
            CoordinatorClaimsDataGrid.ItemsSource = Claims;
            ManagerClaimsDataGrid.ItemsSource = Claims;
        }

        private void RejectClaim_Click(object sender, RoutedEventArgs e)    
        {
            MessageBox.Show("Claim rejected successfully!");
        }


        private void OpenUploadDocs(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog
                {
                    Filter = "Supported Files|*.pdf;*.docx;*.xlsx",
                    Title = "Select a Document"
                };

                if (openFileDialog.ShowDialog() == true)
                {
                    uploadedFilePath = openFileDialog.FileName;
                    UploadedFileNameTextBlock.Text = System.IO.Path.GetFileName(uploadedFilePath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error uploading document: {ex.Message}", "Error",
                                MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void OpenClaimStatus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(LecturerIDTextBox.Text) ||
                    string.IsNullOrEmpty(HoursWorkedTextBox.Text) ||
                    string.IsNullOrEmpty(HourlyRateTextBox.Text))
                {
                    MessageBox.Show("Please fill in all required fields.", "Missing Information",
                                    MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                double hours = double.Parse(HoursWorkedTextBox.Text);
                double rate = double.Parse(HourlyRateTextBox.Text);
                double total = hours * rate;
                TotalAmountTextBox.Text = total.ToString("C");

                Claims.Add(new LecturerClaim
                {
                    ClaimID = Claims.Count + 1,
                    LecturerID = LecturerIDTextBox.Text,
                    HoursWorked = hours,
                    HourlyRate = rate,
                    TotalAmount = total,
                    DateSubmitted = DateTime.Now.ToShortDateString(),
                    Status = "Pending",
                    StatusProgress = 1,
                    Notes = NotesTextBox.Text,
                    AttachedFile = UploadedFileNameTextBlock.Text
                });

                MessageBox.Show("Claim submitted successfully!", "Success",
                                MessageBoxButton.OK, MessageBoxImage.Information);

                LecturerIDTextBox.Clear();
                HoursWorkedTextBox.Clear();
                HourlyRateTextBox.Clear();
                TotalAmountTextBox.Clear();
                NotesTextBox.Clear();
                UploadedFileNameTextBlock.Text = "No file uploaded";
                uploadedFilePath = "";
            }
            catch (FormatException)
            {
                MessageBox.Show("Please enter valid numeric values for hours and rate.", "Invalid Input",
                                MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unexpected error: {ex.Message}", "Error",
                                MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OpenManagerDashboard_Click(object sender, RoutedEventArgs e)
        {

            if (((FrameworkElement)sender).DataContext is LecturerClaim claim)
            {
                claim.Status = "Approved";
                claim.StatusProgress = 3;
                CoordinatorClaimsDataGrid.Items.Refresh();
                ManagerClaimsDataGrid.Items.Refresh();
                LecturerClaimsDataGrid.Items.Refresh();
            }
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)


        {
            if (((FrameworkElement)sender).DataContext is LecturerClaim claim)
            {
                claim.Status = "Rejected";
                claim.StatusProgress = 0;
                CoordinatorClaimsDataGrid.Items.Refresh();
                ManagerClaimsDataGrid.Items.Refresh();
                LecturerClaimsDataGrid.Items.Refresh();
            }

        }
    }
}
