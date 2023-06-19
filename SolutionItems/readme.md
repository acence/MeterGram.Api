
# Work Sample

## What you’ll be doing
You will be creating an API for a fictitious academy. Our academy already has a frontend application (see attached design). This API will let our academy's frontend app accept applications from companies that want to attend the courses we are offering and store them in a database. The courses exist in an external source and you need to get them by consuming the external API (provided below) and expose the courses and their dates in your API so that our frontend app can display them in the dropdowns (see attached design). You also need to offer endpoints that will enable our frontend app to send over the company, course and participant data (enable companies to submit applications for their employees).

Each course is available at different dates. All applications must be saved in a database and contain the following information:

- Course ID
- Course date
- Company name
- Company phone number
- Company e-mail
- 1 or more participants. Each participant consists of the following: name, phone number and e-mail

## Expectations
- Backend: We expect you to create this API using Microsoft .NET, C#, Azure and other related technologies. Most of the data access logic should (prefarably) be done using Entity Framework. A code-first approach is prefered. If you think there is need to include anything else in addition to EF, that is also an acceptable option.

- Frontend: The frontend part should NOT be developed since this will be a mostly backend-oriented position. You can use Postman/Fiddler/Swagger or some other tool to try out the endpoints. You can share with us some exported requests/collections or at least some screenshots and URLs so we can try out the endpoints.

- Define a suitable database structure, and use suitable design patterns throughout.  
The finished work sample should have a readme describing how to get the application up and running.

## Delivery
We want you to return your work to us in the following way:

- **The code** hosted as a repository on Github or similar GIT based service under your own profile. It should be set to private but please share it with (give access to) erik.pegreus@metergram.com
- **A live demo** at a URL where we can view the finished application up and running. Preferably, it is hosted with Azure and take advantage of some of its different services.

When we meet we want you to show us the code and talk about why you've made choices regarding architecture, data structure and how you approached the work.

## Resources
The frontend design is available in the design folder. (NOTE: The design is provided just to make it easier for you to understand how the application is envisioned to work. You should not build the frontend/UI part)

The courses are available via the following API that you need to consume in order to get the courses, store them in your database and expose them via your own API - https://metergram-courses-api.azurewebsites.net/swagger/index.html

- The /api/auth route should be used to get a JWT access token, which should then be used in the GET Courses (/api/courses) request
- The /api/auth route accepts the "apiKey" body parameter with the value of "58cc5cc9-799c-48c1-9af9-c7965b4ac0eb"
- It then returns an access token which you should use in all future requests to the GET Courses endpoint. It should be sent as an Authorization header with the value of "Bearer " + accessToken.
- The access token expires after 20 minutes, and a new one can be issued with the same Api key you used the first time; so you will need to find a way to include a valid token in all future requests to the GET Courses endpoint. You should log the claims that this token contains
- GET Courses is a paginated endpoint, meaning it accepts query string parameters called "offset" and "limit". Offset means - how many items to skip before it starts returning data, and Limit means how many to actually return.
    - Example: Sending - api/courses?offset=10&limit=10  --  means that the first 10 items should be skipped, and the 10 next items should be returned, starting from item number 11. 
    - The "offset" and "limit" parаmeters are optional, so you don't have to include them in the initial request, but obviously they should be included in the follow-up requests
    - The response body contains a property "next_page_link" which contains the link to the next page
- The "isDataUpdated" parameter is equal to false by default and will return an initial set of courses. However, courses might change at any time. So, using this parameter, you should send "true" when you wish to get the updated list of courses. It is enough to sync the courses data once per day. When you get a response with updated courses records, you should insert any new items into your database and update any existing records (if the new response returns items which have also been returned in the initial GET, and thus are already in your database - you should update them because any property might have changed)


## If you have time
The following items are not required but if you have the time feel free to implement (parts of) them. We will review these extras in the same way we review the base work sample.

- **API with endpoints:** Expose the data/applications through an API (documented with e.g. Swagger) so that the academy's other IT-systems can consume them.
- **Unit Test** - Simple unit test to test out any part of the application

## Have fun!
This test is not about pointing fingers or say you did something wrong. We want to see how you think and how you implement a common task into code.
This will be the first impression we get about your coding skills, so make sure you show us the best you can do!