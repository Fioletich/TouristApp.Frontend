var myMap;
var locations;
var multiRoute;     
var points;
var pointFlags = [];
function destroyMap() {
    if (myMap != null) {
        myMap.destroy();
        myMap = null;
    }
}

function buildSearchMapRoute(pinpointsJson) {
    if (multiRoute) {
        myMap.geoObjects.remove(multiRoute);
        points.forEach(point => myMap.geoObjects.remove(point));
    }
    const pinpoints = JSON.parse(pinpointsJson);
    points = pinpoints.map((pinpoint, index) => {
        const placemark = new ymaps.Placemark(pinpoint.coords, {
            balloonContent: createBalloonContent(pinpoint.info, pinpoint.images)
        }, {
            balloonCloseButton: true,
            hideIconOnBalloonOpen: false,
            iconLayout: 'default#image',
            iconImageHref: 'https://cdn-icons-png.flaticon.com/512/252/252025.png',
            iconImageSize: [30, 42],
            iconImageOffset: [-15, -42]
        });
        pointFlags[index] = { placemark, flag: false };
        return placemark;
    });

    points.forEach(point => myMap.geoObjects.add(point));

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
    multiRoute = new ymaps.multiRouter.MultiRoute({
        referencePoints: coords, 
        params: {
            routingMode: 'pedestrian'
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
        
ymaps.ready(initSearchMap);       
function initSearchMap() {
    myMap = new ymaps.Map("searchMap", {
        center: [51.660575, 39.199930], // Воронеж
        zoom: 10
        });
    
    const userPlacemark = new ymaps.Placemark([0, 0], {
            hintContent: "Ваше местоположение"
    });
    myMap.geoObjects.add(userPlacemark);
    
    getUserLocationOnSearch(userPlacemark);
}

function getUserLocationOnSearch(userPlacemark) {
    if (navigator.geolocation) {
        navigator.geolocation.watchPosition(
            (position) => {
                const userCoords = [position.coords.latitude, position.coords.longitude];
                userPlacemark.geometry.setCoordinates(userCoords);
                
                checkProximity(userCoords[0],userCoords[1]);
                }, 
            (error) => {
                console.error("Ошибка получения местоположения: ", error);
                }, 
            { enableHighAccuracy: true }
        );
    } 
    else {
        alert("Ваш браузер не поддерживает геолокацию.");
    }
}

function checkProximity(lat, lon) {
    pointFlags.forEach((entry, index) => {
        const coords = entry.placemark.geometry.getCoordinates();

        if (calculateDistance(lat, lon, coords[0], coords[1]) <= 100 && !entry.flag) {
            alert("Вы рядом с точкой интереса! Предлагаем прослушать информационное аудио!");
            pointFlags[index].flag = true;
            entry.flag = true;
        }
    });
}

function calculateDistance(lat1, lon1, lat2, lon2){
    const R = 6371e3;
    const φ1 = lat1 * Math.PI / 180;
    const φ2 = lat2 * Math.PI / 180;
    const Δφ = (lat2 - lat1) * Math.PI / 180;
    const Δλ = (lon2 - lon1) * Math.PI / 180;

    const a = Math.sin(Δφ / 2) * Math.sin(Δφ / 2) +
        Math.cos(φ1) * Math.cos(φ2) *
        Math.sin(Δλ / 2) * Math.sin(Δλ / 2);

    const c = 2 * Math.atan2(Math.sqrt(a), Math.sqrt(1 - a));

    return R * c; // Расстояние в метрах
}