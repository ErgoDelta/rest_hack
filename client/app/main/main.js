'use strict';

angular
  .module('restHackApp')
  .config(function ($stateProvider) {
    $stateProvider
      .state('main', {
        url: '/',
        templateUrl: 'app/main/main.html',
        controller: 'MainCtrl'
      })
      .state('leaderboard', {
          url: '/Leaderboard',
          templateUrl: 'app/leaderboard/leaderboard.html',
          controller: 'LeaderboardCtrl'
      });
  });
