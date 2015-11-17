in order to get the framework to work on your machine you will need to download PHP to your machine.
Then add the directory to your path under system properties > Environment variables > APPEND the path
to the end of your path by adding a ; followed the path to your PHP folder

Once you have composer downloaded and PHP working in your path (verify by running
                CMD and typing php -version, this will return your PHP Version info if set correctly)
You will need to copy the Composer.phar file into the project directory, and then run from a command line in the
project directory the following command. "php composar.phar update"
this will wire up the classes in the application and set up slim on your machine.

