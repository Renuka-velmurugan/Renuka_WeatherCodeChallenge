# Weather Code challenge

The solution has to determine if a city is having **good weather** or **bad weather** and when is bad weather also get details about it. 

The output should be clearly defined by the candidate and should be reflected in the tests **OpenWeatherMapClientTests** (that is incompleted).

The solution must be and API with one endpoint and two methods GET & POST

POST: 
- To process a petition for obtaining the city weather information the candidate have access to an OpenWeatherMap API to make a request and gather the parameters (aka sensors) about the weather. 
- The solution has an incomplete service *WeatherDetectorService* that should take *sensors* to determine if is having good or bad weather and including the detailed reason if bad weather.
- The weather information must be stored on a repository (preferably in Entity Framework Core, using an in-memory option database will be enough).

GET: 
- Returns a string indicating the last weather.
For example, if we want to look at the weather of Barcelona city, and is raining, the expected output should be something like this:

      "Bad weather in Barcelona because is raining". 

- The information should be obtained from a repository.



An incompleted solution is provided to the candidate that should be finished connecting incomplete parts,
taking into account:

1. Define model Weather properties needed to store all the data.
2. Implement interface IWeatherClient to make the calls to OpenWeatherMap API
3. Implement interface IWeatherRepository to store the defined model Weather
4. Complete the endpoints GET and POST from WeatherController

Weather API [OpenWeatherMap.org](https://openweathermap.org/current)

||  	Example api call to obtain weather from Barcelona 	|
|---	|---	|
| HTTP GET Request 	| <a href="https://api.openweathermap.org/data/2.5/weather?q=barcelona&units=metric&APPID=1b9c96d2448f9204d210ddd5ac192dc1" target="_blank">https://api.openweathermap.org/data/2.5/weather?q=barcelona&units=metric&APPID=1b9c96d2448f9204d210ddd5ac192dc1</a>	|
| Response 	| <pre>{ <br>   "coord": { ... }, <br>   "weather": [{ <br>         "id": 801, <br>         "main": "Clouds", <br>         "description": "few clouds", <br>         "icon": "02d" <br>      }], <br>   "base": "stations", <br>   "main": { ... }, <br>   "visibility": 10000, <br>   "wind": { ... }, <br>   "clouds": { ... }, <br>   "dt": 1618228232, <br>   "sys": { ... }, <br>   "timezone": 7200, <br>   "id": 3128760, <br>   "name": "Barcelona", <br>   "cod": 200 <br>}</pre> 	|

## Goals to evaluate

- How the candidate focus the solution
- How you approach the project 
- Design and architecture
- How the code is writed
- Solution testing

**Please do not hesitate to contact marcal.albert@kantar.com or isaac.diaz@kantar.com if any doubt**
