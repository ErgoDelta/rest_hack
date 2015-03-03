// A singleton game state which can be updated throughout the
// backend application.  This game state holds the current match state.
var Match = function (roster) {

  var nodes,
      _loadMap,
      _assignNodesToUsers,
      TURN_TIME_LIMIT = 60000;

  nodes = [];
  userMoved = {};

  _loadMap = function () {
    // TODO: load the nodes and assign to member variable
  };

  _assignNodesToUsers = function () {
    // TODO: select the 4 start nodes and randomly assign players
    // to those nodes
  }

  _nextTurn = function () {
    setTimeout(endTurn, TURN_TIME_LIMIT);
    _resetUserMoved();
  }

  _resetUserMoved = function () {
    for (var key in userMoved) {
      userMoved[key] = false;
    }
  }

  _haveAllUsersMoved = function () {
    var i;
    for (i = 0; i < users.length; i += 1) {
      user = users[i];
      if (!userMoved[user._id]) {
        return false;
      }
    }
    return true;
  }

  _isOneUserLeftAlive = function () {
    var i,
        node,
        owners;

    owners = {};
    for (i = 0; i < nodes.length; i += 1) {
      node = nodes[i];
      if (owners[node.owner]) {
        owners[node.owner] = true;
      }
    }

    return Object.keys(owners).length === 1;
  }

  startMatch = function () {
    _loadMap();
    _assignNodesToUsers();
    _nextTurn();
  }

  processCommands = function (user, commands) {
    var i,
        command,
        fromNodeId,
        toNodeId,
        numberOfUnits;

    userMoved[user._id] = true;
    for (i = 0; i < commands.length; i += 1) {
      command = commands[i];
      fromNodeId = command.from_node_id;
      toNodeId = command.to_node_id;
      numberOfUnits = command.number_of_units;

      // TODO: the UI needs to visualize these commands
      // Emit via WEBSOCKET to client
    }

    if (_haveAllUsersMoved()) {
      _startTurnTimer();
    }

    if (_isOneUserLeftAlive()) {
      _endMatch();
    }
  }

  _endTurn = function () {

  };

  _endMatch = function () {
    // TODO: award points to the winner
    // TODO: Save the player's score into DB
  };

  return {
    addUser: addUser,
    hasUser: hasUser,
    startGame: startGame,
    startMatch: startMatch,
    startTurn: startTurn,
    endTurn: endTurn,
    endMatch: endMatch
  };

};

module.exports = new Match();
