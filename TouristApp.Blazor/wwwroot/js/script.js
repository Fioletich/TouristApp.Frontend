        var myMap;
        var locations;
           
             
function buildRoute(pinpointsJson) {
    if (myMap.geoObjects.length != 0) {
        myMap.geoObjects.removeAll();
    }

            const pinpoints = JSON.parse(pinpointsJson)
            const points = pinpoints.map(pinpoint => {
                        return new ymaps.Placemark(pinpoint.coords, {
                            balloonContent: createBalloonContent(pinpoint.info, pinpoint.images)
                        }, {
                            balloonCloseButton: true,
                            hideIconOnBalloonOpen: false,
                            iconLayout: 'default#image',
                            iconImageHref: 'https://cdn-icons-png.flaticon.com/512/252/252025.png',
                            iconImageSize: [30, 42],
                            iconImageOffset: [-15, -42]
                        });
                    });
                    
            points.forEach(point => myMap.geoObjects.add(point))
            
            buildRouteByCoords(pinpoints.map(pinpoint => pinpoint.coords));
        }
        function createBalloonContent(info, images) {
                      let content = `<div class="custom-balloon-content">`;
                      images.forEach(image => {
                          content += `<img src="${image}" alt="Image">`;
                      });
                      content += `<p>${info}</p></div>`;
                      return content;
        }
        function buildRouteByCoords(coords) {
                  var multiRoute = new ymaps.multiRouter.MultiRoute({
                          referencePoints: coords, 
                          params: {
                              routingMode: 'auto' 
                          }
                      }, {
                          boundsAutoApply: true
                      });
          
                      myMap.geoObjects.add(multiRoute);
          
                      multiRoute.model.events.add('requestsuccess', function () {
                          var routes = multiRoute.getRoutes();
          
                          routes.each(function (route) {
                              let totalTime = 0;
          
                              route.getPaths().each(function (path) {
                                  path.getSegments().each(function (segment) {
                                      let time = segment.getJamsTime() || segment.getDuration();
                                      totalTime += time;
                                      let duration = Math.floor(time / 60); 
                                      console.log(`Отрезок пути: ${segment.properties.get("text")} - Примерное время: ${duration} мин.`);
                                  });
                              });
          
                              let totalDuration = Math.floor(totalTime / 60);
                              let routeInfo = document.createElement('div');
                              routeInfo.className = 'route-info';
                              routeInfo.innerText = `Общее время маршрута: ${totalDuration} мин.`;
                              document.body.appendChild(routeInfo);
                          });
                      });
        }
                  
        
        
  ymaps.ready(init);
  
      function init() {
          // Создание карты.
          myMap = new ymaps.Map("map", {
              center: [55.751574, 37.573856], // Москва
              zoom: 10
          });
          /*
          // Пример динамически подгружаемых точек
          locations = [
              {
                  coords: [55.751574, 37.573856],
                  info: 'Москва',
                  images: ['https://via.placeholder.com/100', 'https://via.placeholder.com/100']
              },
              {
                  coords: [55.764857, 37.639937],
                  info: 'Кремль',
                  images: ['https://via.placeholder.com/100', 'https://via.placeholder.com/100']
              },
              {
                  coords: [55.749792, 37.632495],
                  info: 'Красная площадь',
                  images: ['https://via.placeholder.com/100', 'https://via.placeholder.com/100']
              }
          ];
  
          // Добавление точек на карту
          const points = locations.map(location => {
              return new ymaps.Placemark(location.coords, {
                  balloonContent: createBalloonContent(location.info, location.images)
              }, {
                  balloonCloseButton: true,
                  hideIconOnBalloonOpen: false,
                  iconLayout: 'default#image',
                  iconImageHref: 'https://cdn-icons-png.flaticon.com/512/252/252025.png',
                  iconImageSize: [30, 42],
                  iconImageOffset: [-15, -42]
              });
          });
  
          points.forEach(point => myMap.geoObjects.add(point));
  
          // Построение маршрута между точками
          buildRoute(locations.map(location => location.coords));
  
          // Функция для создания содержимого балуна с информацией и картинками
          function createBalloonContent(info, images) {
              let content = `<div class="custom-balloon-content">`;
              images.forEach(image => {
                  content += `<img src="${image}" alt="Image">`;
              });
              content += `<p>${info}</p></div>`;
              return content;
          }
  
      function buildRoute(coords) {
          var multiRoute = new ymaps.multiRouter.MultiRoute({
                  referencePoints: coords, 
                  params: {
                      routingMode: 'auto' 
                  }
              }, {
                  boundsAutoApply: true
              });
  
              myMap.geoObjects.add(multiRoute);
  
              multiRoute.model.events.add('requestsuccess', function () {
                  var routes = multiRoute.getRoutes();
  
                  routes.each(function (route) {
                      let totalTime = 0;
  
                      route.getPaths().each(function (path) {
                          path.getSegments().each(function (segment) {
                              let time = segment.getJamsTime() || segment.getDuration();
                              totalTime += time;
                              let duration = Math.floor(time / 60); 
                              console.log(`Отрезок пути: ${segment.properties.get("text")} - Примерное время: ${duration} мин.`);
                          });
                      });
  
                      let totalDuration = Math.floor(totalTime / 60);
                      let routeInfo = document.createElement('div');
                      routeInfo.className = 'route-info';
                      routeInfo.innerText = `Общее время маршрута: ${totalDuration} мин.`;
                      document.body.appendChild(routeInfo);
                  });
              });
          }
          */
              
      }
