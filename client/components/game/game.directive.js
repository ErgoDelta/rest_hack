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

          var colors = ["red", "blue", "green", "purple"];
          var teamColors = { };
          var nodeIndexes = { };
          nodeIndexes.addIndex = function(node, x, y) {
            if(this[node.id.toString()] == undefined)
            {
              this[node.id.toString()] = [x, y];
            }
          };

          function renderNode(nodes) {
            var maxPerRow = Math.sqrt(nodes.length);
            var startingX = (canvas.width / maxPerRow) / 2;
            var startingY = (canvas.height / maxPerRow) / 2;

            if (canvas.width < canvas.height) { var radius = (canvas.width / maxPerRow) * .333; }
            else { var radius = (canvas.height / maxPerRow) * .333; }

            var index = 0;
            for (var y = 0; y < maxPerRow; y++) {
              for (var x = 0; x < maxPerRow; x++) {
                nodeIndexes.addIndex(nodes[index], startingX, startingY);
                context.beginPath();
                context.arc(startingX, startingY, radius,0,2*Math.PI);
                context.fillStyle = getNodeColor(nodes[index]);
                context.fill();
                context.stroke();
                startingX += (canvas.width / maxPerRow);
                index++;
              }
            startingY += (canvas.height / maxPerRow);
            startingX = (canvas.width / maxPerRow) / 2;
            }
          }

          function getNodeColor(node)
          {
            console.log(node.id);
            if (node.owner == "none")
            {
              return "white";
            }
            else
            {
              if (teamColors[node.owner] == undefined)
              {
                teamColors[node.owner] = colors.shift();
              }
              return teamColors[node.owner];
            }
          }

          function buildPaths(nodes)
          {
            for( var nodeIndex = 0; nodeIndex < nodes.length; nodeIndex++){
              var node = nodes[nodeIndex];
              for(var index = 0; index < node.connections.length; index++)
              {
                context.beginPath();
                context.moveTo(nodeIndexes[node.id.toString()][0], nodeIndexes[node.id.toString()][1]);
                context.lineTo(nodeIndexes[node.connections[index]][0], nodeIndexes[node.connections[index]][1]);
                context.stroke();
              }
            }
          }

          scope.$watch("game", function(newValue, oldValue) {
            if(scope.game) {
              renderNode(scope.game.world.nodes);
              buildPaths(scope.game.world.nodes);
            }
          }, true);
      }
    }
  });
