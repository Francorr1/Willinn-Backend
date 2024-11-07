
# Willinn backend test

Technical test for Willinn


## Setup
To get started, clone the repo and run
```bash
docker-compose up --build
```
Once both containers are up, it might take a bit for the SQLServer instance to start and for the api container to apply the necessary migrations.
## Testing
You can either use curl or http://localhost:5000/swagger to test the api.

The included test user is
| Email | Password |
|-------|----------|
|testuser@example.com|SuperSecure1|

The endpoints are the following
| Method | Endpoint    | Description                     |
|--------|-------------|---------------------------------|
| GET    | /users      | Retrieves all users             |
| GET    | /users/{id} | Retrieves a specific user by ID |
| POST   | /users      | Creates a new user              |
| PUT    | /users/{id} | Updates an existing user        |
| DELETE | /users/{id} | Soft deletes a user             |

Structure of a user
| Value name | Value type |
|------------|------------|
| Id         | {int}      |
| Name       | {string}   |
| Email      | {string}   |
| Password   | {string}   |
| IsActive   | {bool}     |