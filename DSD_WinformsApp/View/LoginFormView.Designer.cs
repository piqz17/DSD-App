namespace DSD_WinformsApp.View
{
    partial class LoginFormView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginFormView));
            textBoxSignInUserName = new TextBox();
            textBoxSignInPassword = new TextBox();
            button_SignInButton = new Button();
            labelSignIn = new Label();
            linkLabelSignInToSignUp = new LinkLabel();
            panelSignIn = new Panel();
            buttonEyeIcon = new Button();
            buttonHideEyeIcon = new Button();
            panelSignUp = new Panel();
            textBoxUserJobTitle = new TextBox();
            buttonBackToSignIn = new Button();
            buttonSignUp = new Button();
            textBoxEmailAdd = new TextBox();
            textBoxLastname = new TextBox();
            labelSIgnUp = new Label();
            textBoxFirstname = new TextBox();
            buttonSignUpEyeIcon = new Button();
            buttonSignUpHideIcon = new Button();
            textBoxPasswrd = new TextBox();
            panelSignIn.SuspendLayout();
            panelSignUp.SuspendLayout();
            SuspendLayout();
            // 
            // textBoxSignInUserName
            // 
            textBoxSignInUserName.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            textBoxSignInUserName.Location = new Point(104, 107);
            textBoxSignInUserName.MinimumSize = new Size(350, 45);
            textBoxSignInUserName.Multiline = true;
            textBoxSignInUserName.Name = "textBoxSignInUserName";
            textBoxSignInUserName.PlaceholderText = "Username";
            textBoxSignInUserName.Size = new Size(350, 45);
            textBoxSignInUserName.TabIndex = 0;
            textBoxSignInUserName.TextAlign = HorizontalAlignment.Center;
            // 
            // textBoxSignInPassword
            // 
            textBoxSignInPassword.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            textBoxSignInPassword.Location = new Point(104, 186);
            textBoxSignInPassword.MaximumSize = new Size(350, 45);
            textBoxSignInPassword.Multiline = true;
            textBoxSignInPassword.Name = "textBoxSignInPassword";
            textBoxSignInPassword.PlaceholderText = "Password";
            textBoxSignInPassword.Size = new Size(350, 45);
            textBoxSignInPassword.TabIndex = 1;
            textBoxSignInPassword.TextAlign = HorizontalAlignment.Center;
            // 
            // button_SignInButton
            // 
            button_SignInButton.BackColor = Color.FromArgb(165, 215, 232);
            button_SignInButton.Cursor = Cursors.Hand;
            button_SignInButton.FlatAppearance.BorderSize = 0;
            button_SignInButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(9, 142, 154);
            button_SignInButton.FlatStyle = FlatStyle.Flat;
            button_SignInButton.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point);
            button_SignInButton.Location = new Point(104, 258);
            button_SignInButton.MinimumSize = new Size(350, 45);
            button_SignInButton.Name = "button_SignInButton";
            button_SignInButton.Size = new Size(350, 45);
            button_SignInButton.TabIndex = 2;
            button_SignInButton.Text = "Log In";
            button_SignInButton.UseVisualStyleBackColor = false;
            button_SignInButton.Click += button_SignIn_Click;
            // 
            // labelSignIn
            // 
            labelSignIn.AutoSize = true;
            labelSignIn.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point);
            labelSignIn.Location = new Point(45, 30);
            labelSignIn.Name = "labelSignIn";
            labelSignIn.Size = new Size(81, 28);
            labelSignIn.TabIndex = 5;
            labelSignIn.Text = "Sign In:";
            // 
            // linkLabelSignInToSignUp
            // 
            linkLabelSignInToSignUp.ActiveLinkColor = Color.Blue;
            linkLabelSignInToSignUp.AutoSize = true;
            linkLabelSignInToSignUp.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point);
            linkLabelSignInToSignUp.Location = new Point(104, 330);
            linkLabelSignInToSignUp.Name = "linkLabelSignInToSignUp";
            linkLabelSignInToSignUp.Size = new Size(157, 28);
            linkLabelSignInToSignUp.TabIndex = 6;
            linkLabelSignInToSignUp.TabStop = true;
            linkLabelSignInToSignUp.Text = "Click to Sign Up";
            // 
            // panelSignIn
            // 
            panelSignIn.Controls.Add(buttonEyeIcon);
            panelSignIn.Controls.Add(labelSignIn);
            panelSignIn.Controls.Add(button_SignInButton);
            panelSignIn.Controls.Add(textBoxSignInPassword);
            panelSignIn.Controls.Add(textBoxSignInUserName);
            panelSignIn.Controls.Add(linkLabelSignInToSignUp);
            panelSignIn.Controls.Add(buttonHideEyeIcon);
            panelSignIn.Location = new Point(30, 35);
            panelSignIn.Name = "panelSignIn";
            panelSignIn.Size = new Size(539, 538);
            panelSignIn.TabIndex = 8;
            // 
            // buttonEyeIcon
            // 
            buttonEyeIcon.BackColor = Color.White;
            buttonEyeIcon.BackgroundImage = (Image)resources.GetObject("buttonEyeIcon.BackgroundImage");
            buttonEyeIcon.BackgroundImageLayout = ImageLayout.Stretch;
            buttonEyeIcon.Cursor = Cursors.Hand;
            buttonEyeIcon.FlatAppearance.BorderColor = Color.White;
            buttonEyeIcon.FlatAppearance.BorderSize = 0;
            buttonEyeIcon.FlatStyle = FlatStyle.Flat;
            buttonEyeIcon.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            buttonEyeIcon.Location = new Point(421, 196);
            buttonEyeIcon.Name = "buttonEyeIcon";
            buttonEyeIcon.Size = new Size(25, 29);
            buttonEyeIcon.TabIndex = 4;
            buttonEyeIcon.TabStop = false;
            buttonEyeIcon.UseVisualStyleBackColor = false;
            buttonEyeIcon.Click += buttonEyeIcon_Click;
            // 
            // buttonHideEyeIcon
            // 
            buttonHideEyeIcon.BackColor = Color.White;
            buttonHideEyeIcon.BackgroundImage = (Image)resources.GetObject("buttonHideEyeIcon.BackgroundImage");
            buttonHideEyeIcon.BackgroundImageLayout = ImageLayout.Stretch;
            buttonHideEyeIcon.Cursor = Cursors.Hand;
            buttonHideEyeIcon.FlatAppearance.BorderColor = Color.White;
            buttonHideEyeIcon.FlatAppearance.BorderSize = 0;
            buttonHideEyeIcon.FlatStyle = FlatStyle.Flat;
            buttonHideEyeIcon.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            buttonHideEyeIcon.Location = new Point(416, 196);
            buttonHideEyeIcon.Name = "buttonHideEyeIcon";
            buttonHideEyeIcon.Size = new Size(36, 30);
            buttonHideEyeIcon.TabIndex = 3;
            buttonHideEyeIcon.TabStop = false;
            buttonHideEyeIcon.UseVisualStyleBackColor = false;
            buttonHideEyeIcon.Click += buttonHideEyeIcon_Click;
            // 
            // panelSignUp
            // 
            panelSignUp.Controls.Add(textBoxUserJobTitle);
            panelSignUp.Controls.Add(buttonBackToSignIn);
            panelSignUp.Controls.Add(buttonSignUp);
            panelSignUp.Controls.Add(textBoxEmailAdd);
            panelSignUp.Controls.Add(textBoxLastname);
            panelSignUp.Controls.Add(labelSIgnUp);
            panelSignUp.Controls.Add(textBoxFirstname);
            panelSignUp.Controls.Add(buttonSignUpEyeIcon);
            panelSignUp.Controls.Add(buttonSignUpHideIcon);
            panelSignUp.Controls.Add(textBoxPasswrd);
            panelSignUp.Location = new Point(31, 35);
            panelSignUp.Name = "panelSignUp";
            panelSignUp.Size = new Size(539, 541);
            panelSignUp.TabIndex = 5;
            // 
            // textBoxUserJobTitle
            // 
            textBoxUserJobTitle.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            textBoxUserJobTitle.Location = new Point(45, 207);
            textBoxUserJobTitle.MinimumSize = new Size(440, 45);
            textBoxUserJobTitle.Multiline = true;
            textBoxUserJobTitle.Name = "textBoxUserJobTitle";
            textBoxUserJobTitle.PlaceholderText = "Job Title";
            textBoxUserJobTitle.Size = new Size(440, 45);
            textBoxUserJobTitle.TabIndex = 3;
            textBoxUserJobTitle.TextAlign = HorizontalAlignment.Center;
            // 
            // buttonBackToSignIn
            // 
            buttonBackToSignIn.BackColor = Color.Gray;
            buttonBackToSignIn.Cursor = Cursors.Hand;
            buttonBackToSignIn.FlatAppearance.BorderSize = 0;
            buttonBackToSignIn.FlatAppearance.MouseOverBackColor = Color.FromArgb(9, 142, 154);
            buttonBackToSignIn.FlatStyle = FlatStyle.Flat;
            buttonBackToSignIn.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point);
            buttonBackToSignIn.Location = new Point(335, 434);
            buttonBackToSignIn.Name = "buttonBackToSignIn";
            buttonBackToSignIn.Size = new Size(150, 45);
            buttonBackToSignIn.TabIndex = 6;
            buttonBackToSignIn.Text = "Back";
            buttonBackToSignIn.UseVisualStyleBackColor = false;
            buttonBackToSignIn.Click += buttonBackToSignIn_Click;
            // 
            // buttonSignUp
            // 
            buttonSignUp.Cursor = Cursors.Hand;
            buttonSignUp.FlatAppearance.BorderSize = 0;
            buttonSignUp.FlatAppearance.MouseOverBackColor = Color.FromArgb(9, 142, 154);
            buttonSignUp.FlatStyle = FlatStyle.Flat;
            buttonSignUp.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point);
            buttonSignUp.Location = new Point(141, 434);
            buttonSignUp.Name = "buttonSignUp";
            buttonSignUp.Size = new Size(150, 45);
            buttonSignUp.TabIndex = 5;
            buttonSignUp.Text = "Sign Up";
            buttonSignUp.UseVisualStyleBackColor = true;
            buttonSignUp.Click += buttonSignUp_Click;
            // 
            // textBoxEmailAdd
            // 
            textBoxEmailAdd.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            textBoxEmailAdd.Location = new Point(45, 266);
            textBoxEmailAdd.MinimumSize = new Size(440, 45);
            textBoxEmailAdd.Multiline = true;
            textBoxEmailAdd.Name = "textBoxEmailAdd";
            textBoxEmailAdd.PlaceholderText = "Email Address";
            textBoxEmailAdd.Size = new Size(440, 45);
            textBoxEmailAdd.TabIndex = 4;
            textBoxEmailAdd.TextAlign = HorizontalAlignment.Center;
            // 
            // textBoxLastname
            // 
            textBoxLastname.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            textBoxLastname.Location = new Point(45, 146);
            textBoxLastname.MinimumSize = new Size(440, 45);
            textBoxLastname.Multiline = true;
            textBoxLastname.Name = "textBoxLastname";
            textBoxLastname.PlaceholderText = "Lastname";
            textBoxLastname.Size = new Size(440, 45);
            textBoxLastname.TabIndex = 2;
            textBoxLastname.TextAlign = HorizontalAlignment.Center;
            // 
            // labelSIgnUp
            // 
            labelSIgnUp.AutoSize = true;
            labelSIgnUp.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point);
            labelSIgnUp.Location = new Point(45, 30);
            labelSIgnUp.Name = "labelSIgnUp";
            labelSIgnUp.Size = new Size(91, 28);
            labelSIgnUp.TabIndex = 0;
            labelSIgnUp.Text = "Register:";
            // 
            // textBoxFirstname
            // 
            textBoxFirstname.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            textBoxFirstname.Location = new Point(45, 85);
            textBoxFirstname.MinimumSize = new Size(440, 45);
            textBoxFirstname.Multiline = true;
            textBoxFirstname.Name = "textBoxFirstname";
            textBoxFirstname.PlaceholderText = "Firstname";
            textBoxFirstname.Size = new Size(440, 45);
            textBoxFirstname.TabIndex = 1;
            textBoxFirstname.TextAlign = HorizontalAlignment.Center;
            // 
            // buttonSignUpEyeIcon
            // 
            buttonSignUpEyeIcon.BackColor = Color.White;
            buttonSignUpEyeIcon.BackgroundImage = (Image)resources.GetObject("buttonSignUpEyeIcon.BackgroundImage");
            buttonSignUpEyeIcon.BackgroundImageLayout = ImageLayout.Stretch;
            buttonSignUpEyeIcon.Cursor = Cursors.Hand;
            buttonSignUpEyeIcon.FlatAppearance.BorderColor = Color.White;
            buttonSignUpEyeIcon.FlatAppearance.BorderSize = 0;
            buttonSignUpEyeIcon.FlatStyle = FlatStyle.Flat;
            buttonSignUpEyeIcon.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            buttonSignUpEyeIcon.Location = new Point(444, 340);
            buttonSignUpEyeIcon.Name = "buttonSignUpEyeIcon";
            buttonSignUpEyeIcon.Size = new Size(25, 29);
            buttonSignUpEyeIcon.TabIndex = 9;
            buttonSignUpEyeIcon.TabStop = false;
            buttonSignUpEyeIcon.UseVisualStyleBackColor = false;
            buttonSignUpEyeIcon.Click += buttonSignUpEyeIcon_Click;
            // 
            // buttonSignUpHideIcon
            // 
            buttonSignUpHideIcon.BackColor = Color.White;
            buttonSignUpHideIcon.BackgroundImage = (Image)resources.GetObject("buttonSignUpHideIcon.BackgroundImage");
            buttonSignUpHideIcon.BackgroundImageLayout = ImageLayout.Stretch;
            buttonSignUpHideIcon.Cursor = Cursors.Hand;
            buttonSignUpHideIcon.FlatAppearance.BorderColor = Color.White;
            buttonSignUpHideIcon.FlatAppearance.BorderSize = 0;
            buttonSignUpHideIcon.FlatStyle = FlatStyle.Flat;
            buttonSignUpHideIcon.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            buttonSignUpHideIcon.Location = new Point(440, 340);
            buttonSignUpHideIcon.Name = "buttonSignUpHideIcon";
            buttonSignUpHideIcon.Size = new Size(36, 30);
            buttonSignUpHideIcon.TabIndex = 8;
            buttonSignUpHideIcon.TabStop = false;
            buttonSignUpHideIcon.UseVisualStyleBackColor = false;
            buttonSignUpHideIcon.Click += buttonSignUpHideIcon_Click;
            // 
            // textBoxPasswrd
            // 
            textBoxPasswrd.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            textBoxPasswrd.Location = new Point(45, 330);
            textBoxPasswrd.MinimumSize = new Size(440, 45);
            textBoxPasswrd.Multiline = true;
            textBoxPasswrd.Name = "textBoxPasswrd";
            textBoxPasswrd.PlaceholderText = "Password";
            textBoxPasswrd.Size = new Size(440, 45);
            textBoxPasswrd.TabIndex = 5;
            textBoxPasswrd.TextAlign = HorizontalAlignment.Center;
            // 
            // LoginFormView
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(600, 600);
            Controls.Add(panelSignIn);
            Controls.Add(panelSignUp);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "LoginFormView";
            Text = "Sign Up & Sign In Form";
            Load += LoginFormView_Load;
            panelSignIn.ResumeLayout(false);
            panelSignIn.PerformLayout();
            panelSignUp.ResumeLayout(false);
            panelSignUp.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TextBox textBoxSignInUserName;
        private TextBox textBoxSignInPassword;
        private Button button_SignInButton;
        private Label labelSignIn;
        private LinkLabel linkLabelSignInToSignUp;
        private Panel panelSignIn;
        private Panel panelSignUp;
        private TextBox textBoxFirstname;
        private Label labelSIgnUp;
        private TextBox textBoxPasswrd;
        private TextBox textBoxEmailAdd;
        private TextBox textBoxLastname;
        private Button buttonSignUp;
        private Label labelToastMessage;
        private Button buttonBackToSignIn;
        private Button buttonEyeIcon;
        private Button buttonHideEyeIcon;
        private TextBox textBoxUserJobTitle;
        private Button buttonSignUpHideIcon;
        private Button buttonSignUpEyeIcon;
    }
}