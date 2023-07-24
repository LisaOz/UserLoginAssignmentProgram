
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

class UserLogin
{
    static Dictionary<string, string> userDetails = new Dictionary<string, string>();


    static void Main(string[] args)

    {

        // Program displays a greeting message and the menu of options to the user  
        Console.WriteLine("***********************************************");
        Console.WriteLine("*                                             *");
        Console.WriteLine("*..*..*..*  Welcome to our Program!  *..*..*..*");
        Console.WriteLine("*                                             *");
        Console.WriteLine("*                                             *");
        Console.WriteLine("*                                             *");
        Console.WriteLine("*              Choose an option:              *");
        Console.WriteLine("*                                             *");
        Console.WriteLine("*                 1. Login                    *");
        Console.WriteLine("*                                             *");
        Console.WriteLine("*                 2. Register                 *");
        Console.WriteLine("*                                             *");
        Console.WriteLine("*                 3. Exit                     *");
        Console.WriteLine("*                                             *");
        Console.WriteLine("*                                             *");
        Console.WriteLine("***********************************************");


        // Code Used to control the flow of the program; creates a loop  which repeatedly displays the login menu until they choose to exit
        bool exitProgram = false;
        bool loggedIn = false;



        while (!exitProgram)
        {
            if (!loggedIn)
            {
                Console.WriteLine("Please enter your choice: "); // prompts the user to enter one of the options
            }
            else
            {
                Console.WriteLine("Please, log in or register: ");
            }

            string input = Console.ReadLine();
            Console.Clear(); //  clears the console screen and removes any previous output before displaying new output



            switch (input)
            {
                case "1":
                    Login();
                    Console.WriteLine("You are successfully logged in");
                    loggedIn = true; 
                    Main(null); // returns the user the the main menu
                    break;

                case "2":
                    Register();
                    Console.WriteLine("You are successfully registered");
                    Main(null); // returns the user to the main menu
                    break;

                case "3":
                    exitProgram = true;
                    Console.WriteLine("Goodbye! See you next time!"); // the farewell message if the user chooses to exit the program
                    break;

                default:

                    Console.WriteLine("Invalid input. Please choose to login or to register"); // message to the user in case the input is not defined 
                    break;
            }


            if (exitProgram)
            {
                break;
            }
        }

        Console.WriteLine("Press any key to exit"); // message to the user to press any key to exit
        Console.ReadKey(); // any key pressed by the user is being read by the program as an input
        Console.WriteLine();
        Console.WriteLine("You are exitting the program...  "); // displayed message while the system is waiting 3 second for being closed
        Thread.Sleep(3000); // code to pause for 3 seconds before exiting the program
        Environment.Exit(0); // closes the console window
    }




    static void Register()

    {
        string email, username, password; // initializes email, username and password variables

        while (true) // loops until a valid email is entered
        {

            Console.WriteLine("Please enter your email: "); // prompt the user to enter their email
            email = Console.ReadLine(); // saves the email in a variable

            if (string.IsNullOrWhiteSpace(email)) // checks if the input is empty or only white space
            {
                Console.WriteLine("Invalid input. Please try again.");
                continue;
            }

            if (!IsValidEmail(email)) // checks if the email is valid
            {
                Console.WriteLine("Invalid input. Please enter a valid email.");
                continue;
            }

            if (userDetails.ContainsKey(email))// checks if the email already exists in the dictionary
            {
                Console.WriteLine("Email already exists. Please choose a different email.");
                continue;
            }

            break; // If the email is valid and unique, breaks the loop
        }




        bool IsValidEmail(string email) //Checks if the email matches the regular expression for a valid email address
        {
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"); // standart code to check whether the email contains these characters: @ and . 

        }


        // Handling username input


        while (true) // loops until a valid username is entered
        {
            Console.WriteLine("Please, enter your username: "); // prompts the user to enter their username
            username = Console.ReadLine();  // saves the username in a variable


            if (string.IsNullOrWhiteSpace(username))
            {
                Console.WriteLine("Invalid input. Please try again."); // loops will the valid username is entered
                continue;
            }

            if (username.Length < 3 || username.Length > 12) // places the restriction for the username length
            {
                Console.WriteLine("Invalid input. Your username must be least 3 and not more then 12 characters");
                continue;
            }

            if (userDetails.ContainsKey(username)) // checks whether the specified key is present in the disctionary
            {
                Console.WriteLine("Username already exists. Please choose a different username");
                continue;
            }

            break;

        }


        // Prompts the user to create a password and checks is validity


        while (true) // loops untill a password is entered
        {
            Console.WriteLine("Please enter your password: "); // prompts user to enter the password

            password = Console.ReadLine(); // reads the user's input from the console and assigns it to the variable 'password'


            if (string.IsNullOrWhiteSpace(password)) //checks if the password contains white spaces or if it is not enetered
            {
                Console.WriteLine("Invalid input.Try again.");
                continue;
            }


            if (password.Length < 8 || password.Length > 12 || !HasUppercaseLetter(password) || !HasLowercaseLetter(password) || !HasNumber(password))
            {
                Console.WriteLine("Invalid input. Your password must have at least 8 and at most 12 characters, and have at least one uppercase character, one lowercase character and one digit.");
                continue;

            }
            break;
        }


        // saves the user's credentials into the dictionary
        userDetails.Add(email, password); 
        userDetails.Add(username, password);
    }

    // checks the password's validity: whether it contains uppercase, lowercase letter and a digit
    static bool IsPasswordValid(string password)
    {
        return !password.Contains("password");
    }

    static bool HasUppercaseLetter(string password)
    {
        return password.Any(char.IsUpper);
    }

    static bool HasLowercaseLetter(string password)
    {
        return password.Any(char.IsLower);
    }

    static bool HasNumber(string password)
    {
        return password.Any(char.IsNumber);
    }

    // This is a code to prompt the user to enter a password without displaying the password characters on the screen as they are typed.

    string ReadPassword()
    {
        string password = ""; // creates a new empty string variable which will be used to store the user's password input
                              // do-while loop to continuously read in keys from the console until the Enter key is pressed.

        do
        {
            ConsoleKeyInfo key = Console.ReadKey(true); // creates a variable to store the user's keystrokes on the keyboard as the user types the password. 
            if (key.Key == ConsoleKey.Enter) // checks if the user pressed the 'Enter' key, if pressed, the conditional statements is true and the next code will be executed
            {
                break;                                        
            }


            if (key.Key != ConsoleKey.Backspace) // checks if the user pressed a backspace key
            {
                if (password.Length > 0)
                {
                    password = password.Remove(password.Length - 1); // the last character in the password string is removed 

                    Console.Write("\b \b"); // code to erase the last character that was written to the console output with a backspace key; to correct the mistaken input
                }
            }

            else if (char.IsLetterOrDigit(key.KeyChar)) // if the input is a letter or a digit, it is being entered in to console, one by one
            {
                password += key.KeyChar; // code to add a character  from the console to                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   the string variable 'password'
                Console.Write("*"); // masks the input with the asterisks
            }
        } while (true);


        return password; //the password string is returned to the calling function, if it is valid and match all the requirements

    }

    static void Login()
    {

        bool isLoggedIn = false;    // allows to see whether the user was successfully logged in
        int loginAttempts = 0;      // variable to see how many attempts to login the user has made
        string username = "";       // variable to store the user's username
        string password = "";       // variable to store the user's password
        string email = "";          // variable to store the user's email


        /* creates a while-loop with the condition that currently the user is not logged in
         * and a number of attempts that were not exceeded */
       
            while (!isLoggedIn && loginAttempts < 3)
            {
                Console.WriteLine("Please enter your username: "); // Prompts the user to enter the username into the console
                username = Console.ReadLine(); // stores the username in a variable
                loginAttempts++; // increments the number of login attempts until they reach 3

            // try-catch block for detecting errors while entering username or password
            try
                {

                    if (!IsStringValid(username))
                    {
                        throw new ArgumentException("Invalid Input. Please enter your username.");
                    }
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine(e.Message); 

                    loginAttempts++; 
                    continue;
                }

                static bool IsStringValid(string username)
                {
                    if (string.IsNullOrEmpty(username)) // if the input for the username is empty, it will return a mistake
                    {
                        return false;
                    }
                    else if (username.Length < 3 || username.Length > 12) // sets the limit for the username string length,
                    {
                        return false; //  if the username is shorter then 3 and longer than 12 characters, it will return false
                    }
                    return true; // if it has the length between 3 and 12 characters, it will return true
                }

                // Password being entered and checked whether it is valid and matches the password saved into the userDetails
                Console.WriteLine("Enter your password:\n"); // prompts the user to enter the password, it will be displayed on the new line



                // Masks the password input 
                string enteredPassword = ""; // declares and initializes a new string variable with an empty string value
                ConsoleKeyInfo key;
                do
                {
                    key = Console.ReadKey(true);
                    if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter) // if the presses key are not 'enter' and 'backspace#
                    {

                        enteredPassword += key.KeyChar; // adds the character represented by the key just presses to a string variable 'enteredPassword'
                        Console.Write("*"); // masks the input with the asterisks

                    }
                    else
                    {
                        if (key.Key == ConsoleKey.Backspace && enteredPassword.Length > 0)
                        {
                            enteredPassword = enteredPassword.Substring(0, enteredPassword.Length - 1);
                            Console.Write("\b \b");
                        }
                    }
                } while (key.Key != ConsoleKey.Enter);

                password = enteredPassword; // the value of the 'entered password' is assigned to the 'password'

                try
                {
                    if (!IsPasswordValid(password)) // if the password is not valid, the exception message is thrown
                    {
                        throw new ArgumentException("Your password is incorrect. Please try again.");
                    }

                }

                catch (ArgumentException e)
                {
                    Console.WriteLine("\n" + e.Message);
                    loginAttempts++; // increments the number of login attempts until they reach 3

                    continue;
                }

                if (!userDetails.ContainsKey(username) && loginAttempts < 3)
                {
                    Console.WriteLine("\nYour username or password is incorrect. Please check your details and try again.");
                }
                else
                {

                    /* This code is checking if the provided username exists in the userDetail dictionary 
                    * and if the corresponding password matches the one provided by the user. 
                    * If they match the user is logged in, if not the user is prompted to try again or exit the program.
                    * Note: the userDetails dictionary saves users credentials, Keys are usernames and values are passwords*/

                    string savedPassword = userDetails[username];
                    if (password == savedPassword)
                    {
                        isLoggedIn = true; // login is successful, set isLoggedIn to true
                        Console.WriteLine();
                        Console.WriteLine($"Welcome to our Program {username}!"); // Welcome message when login is successful
                        loginAttempts++; // increment login attempts when the user logs in successfully


                    }
                    else
                    {

                        Console.WriteLine("\nYour password is incorrect. Please check your details and try again");

                        if (loginAttempts < 3) // code that sets the conditions  to limit the login attempts to 3 times
                        {

                            Console.WriteLine("Press \"Y\" to try again or \"N\" to return to the main menu");
                            var input = Console.ReadLine();

                            switch (input.ToUpper()) // the user can input either lowercase or uppercase characters, it will be turned to uppercase
                            {
                                case "Y":
                                    loginAttempts++;  // increments the login attempts until they reach 3
                                    if (loginAttempts >= 3)
                                    {
                                        Console.WriteLine($"{username}, you have exceeded the maxinum number of login attempts ({loginAttempts})");
                                        Console.WriteLine("Please, reset your password.");
                                        ResetPassword(username); // reset password function is called, to reset the password for the user with the given username

                                        return;
                                    }
                                    break;


                                case "N":
                                    Main(null);
                                    return; // add a return statement to terminate the function 

                                default:
                                    Console.WriteLine("Invalid input");
                                    break;
                            }

                        }
                        else

                        {
                            Console.WriteLine($"You have exceeded the maximum number of Login attempts ({loginAttempts}). Please reset your password");
                            ResetPassword(username);

                            return;
                        }
                    }
                }
            }
        }

      


        // Code that allows the user to reset the password
        static void ResetPassword(string username)
            {
        Console.WriteLine($"Resetting password for user {username}"); // Message to the user that the password is being resetting
            string newPassword; // initialize the new password

            while (true)
            {
                Console.WriteLine("Enter new password: "); // prompts the user to enter a new password
                newPassword = Console.ReadLine(); // call ReadPassword to read in the new passowrd


                // Check if the new password meets the requirements
                if (newPassword.Length < 8 || newPassword.Length > 12 || !HasUppercaseLetter(newPassword) || !HasLowercaseLetter(newPassword))
                {
                    Console.WriteLine("Invalid input. Your password must have at least 8 and at most 12 characters, and contain at least 1 uppercase, 1 lowercase letter and 1 digit");
                    continue;

                }
                break; // breaks the loop of the new password meets the requirements

            }

               
                userDetails[username] = newPassword; // Updates the password in the userDetails dictionary
                Console.WriteLine("Password update successful. Please login with the new password."); // Message to the user that the passwrod is updated with the new one 
                Main(null); // redirects the user to the main menu
            }
        }
    