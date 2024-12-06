﻿var myMap;
var locations;
var multiRoute;     
var points; 
             
function destroyMap() {
    if (myMap != null) {
        myMap.destroy();
        myMap = null;
    }
}  

function buildSearchMapRoute(pinpointsJson) {
    if (multiRoute) {
        myMap.geoObjects.remove(multiRoute);   
        points.forEach(point => myMap.geoObjects.remove(point))
    }

    const pinpoints = JSON.parse(pinpointsJson)
    points = pinpoints.map(pinpoint => {
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
                userPlacemark.geometry.setCoordinates([x,y]);
                },
                (error) => {
                    console.error("Ошибка получения местоположения:", error);
                },
                { enableHighAccuracy: true }
            );
    } 
    else {
        alert("Ваш браузер не поддерживает геолокацию.");
    }
}

function checkProximity(x,y){
    const distance = calculateDistance(x,y);
    points.forEach(point=>{if (distance<=0.1){
        const info = point.get('balloonContent'); 
        const audioUrl = extractAudioUrl(info);
        
        if(audioUrl){
            playAudio(audioUrl);
        }
    }})
}

function extractAudioUrl(info) {
    const urlPattern = /https?:\/\/(?:www\.)?[a-zA-Z0-9-]+\.[a-zA-Z]{2,6}(?:\/[^\s]*)?/g;
    const match = info.match(urlPattern);

    if (match && match.length > 0) {
        return match[0];
    }
    return null;
}
function calculateDistance(x,y){
    return sqrt(x*x + y*y);
}

function playAudio(audioUrl){
    if (!audioPlayed){
        showPopup(audioUrl);
        audioPlayed = true;
        
        setTimeout(()=>{audioPlayed = false;}, 50000);
    }
}

function showPopup(audioUrl){
    const popUp=document.getElementById('audPopUp');
    const Player=document.getElementById('Player');
    const audsrc=document.getElementById('Source');
    
    popUpTtl.textContent=title;
    audsrc.src=audioUrl;
    Player.load();
    Player.play();
    
    popUp.classList.remove('hidden');
}

function hidePopup(){
    const popUp=document.getElementById('audPopUp');
    const Player=document.getElementById('Player');
    Player.stop();
    popUp.classList.add('hidden');
}
