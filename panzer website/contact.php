<!DOCTYPE html>

<html>
    <head>
        <meta charset="UTF-8">
        <title>Contact</title>
        <link rel="stylesheet" href="css/style.css" type="text/css">
    </head>
    <body>
        <div class="page">
            <div class="header header-contact">
                <a href="index.php" id="logo"><img src="images/logo.png" alt="logo"></a>
                <ul>
                    <li>
                        <a href="index.php">Home</a> <span></span>
                    </li>
                    <li>
                        <a href="about.php">About</a> <span></span>
                    </li>
                    <li>
                        <a href="technology.php">Technology</a> <span></span>
                    </li>
                    <li class="selected">
                        <a href="contact.php">Contact</a> <span></span>
                    </li>
                </ul>
                <div class="featured">

                    <h3>Contact Us!</h3>
                    <p>
                        No one can develop bug free software. Panzer also not perfect. If there are errors or any problem regarding Panzer, feel free to contact us.... 
                    </p>
                </div>
            </div>
            <div class="body">
                <div class="sidebar">
                    <div>
                        <h3>Connect</h3>
                        <a href="http://freewebsitetemplates.com/go/twitter/" id="twitter">twitter</a> <a href="http://freewebsitetemplates.com/go/facebook/" id="fb">fb</a> <a href="http://freewebsitetemplates.com/go/googleplus/" id="googleplus">google+</a>
                    </div>


                    <div>
                        <div>
                            <h3>CSE Mission </h3>
                            <ul>
                                <li>
                                    <h4><a></a></h4>
                                    <p>
                                        The mission of the Department of Computer Science and Engineering is to serve society through excellence in education, research and service. We provide for our students an education in computer science and computer engineering and we attempt to instil in them the attitudes and values that will prepare them for a lifetime of continued learning and leadership. Through research the Department strives to generate new knowledge and technology for the benefit of everyone.
                                    </p>
                                </li>

                            </ul>
                        </div>
                    </div>

                </div>
                <div class="content contact">

                    <div class="content">
                        <ul>
                            <li>
                                <span></span> <a><img src="images/chathura.jpg" alt=""></a>
                                <div>
                                    <h3>Chathura Wijeweera</h3>
                                    <p>
                                        AI developer<br>Department of Computer Science and Engineering<br>University of Moratuwa<br>Index-120734X<br>mobile-0716469599
                                    </p>

                                </div>
                            </li>
                            <li>
                                <span></span> <a><img src="images/hasintha.jpg" alt=""></a>
                                <div>
                                    <h3>A M H S Randika</h3>
                                    <p>
                                        Game developer and graphic designer<br>Department of Computer Science and Engineering<br>University of Moratuwa<br>Index-120526L<br>mobile-0718217750
                                    </p>

                                </div>
                            </li>

                            <li>
                                <span></span>
                                <h3>Submit your comments</h3>
                                <form  role="form" action="contact.php" method="post">
                                    <table style="color:darkred" width="400">

                                        <tr height="50"><td>Name:</td><td><input type="text" required name="name"></td></tr>
                                        <tr height="50"><td>E-Mail:</td><td><input type="email" required name="email"></td></tr>
                                        <tr height="50"><td>Comment:</td><td><input type="text" required name="comment"></td></tr>
                                        <tr height="50"><td></td><td><input type="submit" onclick="myFunction()" style="color:darkred"></td></tr>
                                         <script>
                                            function myFunction() {
                                            if(!empty("name")){
                                                alert("Your Comment was sent successfully");
                                            }
                                        }
                                            
                                        </script>

                                        

                                    </table>
                                </form>


                            </li>
                        </ul>
                    </div>
                   
                </div>
                <p>&nbsp;</p>
            </div>
    </body>
</html>