'use strict';

/**
* Used to draw and render the game as well as take in games inputs in the view.
*/
angular
  .module('restHackApp')
  .directive("game",
    ['d3Service', '$window',function(d3Service, $window) {
      return {
        restrict: "EA",
        scope: { game: '=' },
        link: function(scope, element, attr) {
          d3Service.d3().then(function(d3) {
            scope.svg = d3.select(element[0])
              .append('svg')
              .attr("width", element.width())
              .attr("height", element.width());

            scope.board = scope.svg
              .append('rect')
              .attr('x', 0)
              .attr('y', 0)
              .attr('width', element.width())
              .attr('height', element.width())
              .attr('fill', 'black');

            scope.gameText = scope.svg
              .append('text')
              .attr('x', 20)
              .attr('y', 80)
              .attr('font-size', '60px')
              .attr('fill', 'white');


            // Browser onresize event
            window.onresize = function() {
              scope.$apply();
            };

            // Watch for resize event
            scope.$watch(function() {
              return angular.element($window)[0].innerWidth;
            }, function() {
              scope.render(scope.data);
            });

            scope.$watch("game", function(newValue, oldValue) {
              if(scope.game) {
                if(!scope.bootStrapped){
                  scope.bootStrapBoard();
                  scope.bootStrapped = true;
                }
                scope.render(scope.data);
              }
            }, true);

            scope.bootStrapBoard = function(){
              scope.nodes = [];
              scope.links = [];

              for(var i = 0; i <  scope.game.world.nodes.length; i++){
                var node = scope.game.world.nodes[i];
                for (var j = 0; j < node.connections.length; j++) {
                  scope.links.push({source: i, target: node.connections[j] , value:1 });
                }

                scope.nodes.push(scope.game.world.nodes[i]);

              }
              console.log(scope.links);

              var force = d3.layout.force()
                .charge(-200)
                .linkDistance(500)
                .size([element.width(), element.width()]);

              force
                .nodes(scope.nodes)
                .links(scope.links)
                .start();

              var link = scope.svg.selectAll(".link")
                .data(scope.links)
                .enter().append("line")
                .attr("class", "link")
                .style("stroke", "white")
                .style("stroke-width", "1");

              var node = scope.svg.selectAll(".node")
                .data(scope.nodes)
                .enter().append("circle")
                .attr("class", "node")
                .attr("r", 50)
                .style("fill", "white")
                .call(force.drag);

              var padding = 125, // separation between circles
                radius=50;

              function collide(alpha) {
                var quadtree = d3.geom.quadtree(scope.nodes);
                return function(d) {
                  var rb = 2*radius + padding,
                    nx1 = d.x - rb,
                    nx2 = d.x + rb,
                    ny1 = d.y - rb,
                    ny2 = d.y + rb;

                  quadtree.visit(function(quad, x1, y1, x2, y2) {
                    if (quad.point && (quad.point !== d)) {
                      var x = d.x - quad.point.x,
                        y = d.y - quad.point.y,
                        l = Math.sqrt(x * x + y * y);
                      if (l < rb) {
                        l = (l - rb) / l * alpha;
                        d.x -= x *= l;
                        d.y -= y *= l;
                        quad.point.x += x;
                        quad.point.y += y;
                      }
                    }
                    return x1 > nx2 || x2 < nx1 || y1 > ny2 || y2 < ny1;
                  });
                };
              }

              force.on("tick", function() {
                link.attr("x1", function(d) { return d.source.x; })
                  .attr("y1", function(d) { return d.source.y; })
                  .attr("x2", function(d) { return d.target.x; })
                  .attr("y2", function(d) { return d.target.y; });

                node.attr("cx", function(d) { return d.x; })
                  .attr("cy", function(d) { return d.y; });

                node.each(collide(0.5)); //Added
              });



            };

            scope.render = function(data) {
              console.log(element.width());
              scope.gameText.text(scope.game.name);
              scope.svg
                .attr("width", element.width())
                .attr("height", element.width());

              scope.board
                .attr("width", element.width())
                .attr("height", element.width());

            }
          });
      }
    }
  }]);
