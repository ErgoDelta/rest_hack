var Scoreboard = function () {
  var awardPointsToUser,
      getPoints,
      points,
      POINTS_PER_WIN;

  points = {}
  POINTS_PER_WIN = 2;

  awardPointsToUser = function (user) {
    if (!points[user._id]) {
      points[user._id] = 0;
    }

    points[user.id] += POINTS_PER_WIN;
  }

  getPoints = function () {
    return points;
  }

  return {
    getPoints: getPoints,
    awardPointsToUser: awardPointsToUser
  };

};

module.exports = new Scoreboard();
