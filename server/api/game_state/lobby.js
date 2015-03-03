var Lobby = function () {
  var rosters,
      users,
      addUser,
      hasuser,
      startGame,
      startMatch,
      startTurn,
      processCommands,
      endTurn,
      endMatch,
      started,
      _getRandomUserId;

  matchups = [];
  users = [];
  userIdsWithoutRoster = [];
  started = false;
  USERS_PER_ROSTER = 4;

  _getNextRandomUser = function () {
    var randomI;
    randomI = Math.parseInt(Math.random() * userIdsWithoutMatchup.length);
    return userIdsWithoutMatchup[randomI];
  }

  addUser = function (user) {
    if (!started) {
      users.push(user);
      userIdsWithoutMatchup.push(user._id);
    }
  }

  hasUser = function (user) {
    var i,
        user;
    // TODO: Use underscore or lodash framework for doing this logic
    for (i = 0; i < users.length; i += 1) {
      user = users[i];
      if (user._id = user._id) {
        return true;
      }
    }
    return false;
  }

  startGame = function () {
    var roster,
        randomUserId,
        randomUser,
        i;

    // TODO: Figure out issues when a roster may be created with 1-3 users, maybe add easy bots?
    while (userIdsWithoutRoster.length > 0) {
      roster = new Roster();
      for (i = 0; i < USERS_PER_ROSTER && userIdsWithoutRoster.length > 0; i += 1) {
        randomUserId = getRandomUserId();
        randomUser = users[randomUserId];
        userIdsWithoutMatchup.splice(
          userIdsWithoutMatchup.indexOf(randomUserId), 1);
        roster.addUser(randomUser);
      }
      rosters.push(roster);
    }

    started = true;

  }

  endGame = function () {
    // TODO: Finalize all scoring / disable game logic
  };

  getNextMatchup = function () {
    return rosters.shift();
  }

  return {
    addUser: addUser,
    hasUser: hasUser,
    startGame: startGame,
    endGame: endGame,
    getNextMatchup: getNextMatchup
  };

};

module.exports = new Lobby();
