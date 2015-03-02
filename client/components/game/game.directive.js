'use strict';

/**
* Used to draw and render the game as well as take in games inputs in the view.
*/
angular
  .module('restHackApp')
  .directive("game",
    function() {
      return {
        restrict: "EA",
        scope: { game: '=' },
        link: function(scope, element, attr) {
          var canvas = document.getElementById('game_canvas'),
              context = canvas.getContext('2d');

          function renderNode(node) {
            for (var i = node.id * 2; i <= node.id * 10; i = i + 5) {
              context.beginPath();
              context.moveTo(i, 0);
              context.lineTo(i, 1000);
              context.stroke();
            }
          }

          scope.$watch("game", function(newValue, oldValue) {
            if(scope.game) {
              for (var node in scope.game.world.nodes) {
                renderNode(scope.game.world.nodes[node]);
              }
            }
          }, true);
      }
    }
  });
