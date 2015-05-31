<!DOCTYPE html>

<html>
<head>
	<meta charset="UTF-8">
	<title>About</title>
	<link rel="stylesheet" href="css/style.css" type="text/css">
</head>
<body>
	<div class="page">
		<div class="header header-about">
		  <ul>
			  <li>
					<a href="index.php">Home</a> <span></span>
				</li>
				<li class="selected">
					<a href="about.php">About</a> <span></span>
				</li>
				<li>
					<a href="technology.php">Technology</a> <span></span>
				</li>
				<li>
					<a href="contact.php">Contact</a> <span></span>
				</li>
			</ul>
			<div class="featured">
				<h2>Read before you play</h2>
				<span></span>
			</div>
		</div>
		<div class="body">
			<div class="sidebar">
				
				<div>
					<div>
						<h3>Miscellaneous link one</h3>
						<ul>
							<li>
								<h4><a>CSE Vision </a></h4>
								<p>
									The Department of Computer Science and Engineering will be the most exciting place in the country and the Asian region to perform high impact research and to learn about the latest developments in the rapidly developing field of computer science and engineering.
We will become the best CSE Department by the end of this decade, as measured by

    the excellent preparation of our graduates for leadership in the profession,
    the quality and impact of our research and
    the exceptional value of our service to the Nation. 
								</p>
							</li>
							
						</ul>
					</div>
				</div>
				
			</div>
			<div class="content">
				<h4>Onece a <span>Soldier</span>, always a <span>Soldier</span></h4>
				<h3>Introduction</h3>
			  <p>
					The game objective is; clients acting as “tanks” accumulating points while making sure of their own survival.
The tanks are capable of shooting shells (bullets) and these bullets will move 3 times faster than a tank.
The environment is made out of four kinds of blocks
Brick wall 
Stone wall
Water
Blank cell


				</p>
				<h3>Game  Initiation</h3>
				<p>
A timer is initiated when the first player registers for a game.
When the 5th player joins the game or in the event of the timer expiring, the server will issue the game starting message to all the players that are already registered with the server.
Any join request by a client hereafter until the end of the game instance will be rejected.
The game will be played in a 20x20 grid.



			  </p>
				<h3>Acquiring coins</h3>
				<p>
					For two reasons coins will appear on the map. 
Random treasures
Spoils of war

The players are supposed to collect them by moving to the cell where the coin pile is.

When a player collects a pile of coins, his coin count and point count gets increased by that amount.
</p>
				<h3>Acquiring points</h3>
				<p>
					There are three ways to acquire points 
Breaking bricks
Each time a shell (bullet) from a player damages a brick wall, a constant amount of points is added to the player’s point count.
Collecting coins
As described in the previous topic
Spoils of war
Will be discussed in a subsequent topic

				</p>
                <h3>Life Packs</h3>
				<p>
					Similar to the piles of coins, life packs will appear on empty cells.
The method of taking a life pack is similar to the method of taking a treasure.
When a client takes a life pack, 20% of the initial health will be added to their health.
Note  that it is possible for a client, at some point of the game, to have a health value which is greater than the initial health value.


				</p>
                
					<h3>Killing</h3>
				<p>
					Similar to the piles of coins, life packs will appear on empty cells.
The method of taking a life pack is similar to the method of taking a treasure.
When a client takes a life pack, 20% of the initial health will be added to their health.
Note  that it is possible for a client, at some point of the game, to have a health value which is greater than the initial health value.


				</p>
<h3>Penalties and Death</h3>
				<p>
					If a client hits an obstacle (a standing brick wall or a stone wall), a fixed number of points will be deducted from the client.
The deduction takes place per each turn, thus if a client keeps hitting an obstacle, it will continuously lose points. clients can shoot over water. But if the client tries to move the tank in to a cell with water, the client will be killed.
Since there is no killer here other than the environment, no body will be credited with points and the coins of the slain client will not be dropped on the ground.
But dead clients  get their coin count set to zero nevertheless. 




				</p>
				</p>
                
			</div>
		</div>
		<div class="footer">
			<ul>
				
			</ul>
			
		</div>
	</div>
</body>
</html>