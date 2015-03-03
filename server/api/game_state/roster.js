var Roster = function () {

  var bots = [];

  var addBot = function (bot) {
    bots.push(bot);
  }

  var getBots = function () {
    return bots;
  }

  return {
    getBots: getBots
  };

};

module.exports = Roster;
