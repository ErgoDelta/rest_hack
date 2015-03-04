angular.module('restHackApp')
    .controller('LeaderboardCtrl', ['$http', function ($http) {
        var leaderboard = this;
        leaderboard.players = []

        $http.get('/api/leaderboard').success(function (data) {
            leaderboard.players = data;
            for (i = 0; i < leaderboard.players.length; i++) {
                //Calculate the winning percentage for each team.
                leaderboard.players[i].winningPercentage = leaderboard.players[i].gamesWon / leaderboard.players[i].gamesPlayed;
            }
        });
}]);
