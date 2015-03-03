request = require('request-json');

var request,
    User,
    users,
    user,
    client;

client = request.createClient('http://localhost:9000/');

User = function (email, password) {
  this.email = email;
  this.password = password;
  this.token = undefined;
}

users = [
  new User('bot1@gmail.com', '123456'),
  new User('bot2@gmail.com', '123456'),
  new User('bot3@gmail.com', '123456'),
  new User('bot4@gmail.com', '123456')
]

// Login all users
for (i = 0; i < users.length; i += 1) {
  user = users[i];
  client.post('auth', {email: user.email, password: user.password}, function(err, res, body) {
    token = body.token;
    user.token = token;
    user._id = body._id;
  });
}

// At this point, an ADMIN needs to send a "start" command


// Make the user's bots think every second
setInterval(function () {
  var nodes;

  // Make request for the game state 'nodes'
  client.get('nodes', {}, function(err, res, body) {
    nodes = body;

    // Make sure the game has started
    if (state === undefined) {
      return;
    }

    // all users need to send commands now
    for (i = 0; i < users.length; i += 1) {
      user = users[i];

      // find the nodes the user controllers
      for (var j = 0; j < nodes.length; j += 1) {
        node = nodes[j];

        // Find nodes the user owns
        if (node.owner !== user._id) {
          continue;
        }

        // the user owns the node, so send a random amount of units
        // out from that node to an adj node
        adj = nodes.adjacent;
        randomCount = parseInt(Math.random() * nodes.count);
        randomAdjacentNodeIndex = parseInt(Math.random() * nodes.adjacent.length);
        randomAdj = nodes[randomAdjacentNodeIndex];

        // send out the request to the server
        var command = {
          from_node_id: node.id,
          to_node_id: randomAdj.id,
          count: randomCount
        };
        client.post('commands', command, function(err, res, body) {

        });

      }
    }
  });
}, 1000);
