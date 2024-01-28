## Auth
### Register

```js
POST {{host}}/auth/register
```

#### Register Request
```json
{
    "firstName":"Dylan",
    "lastName":"Chen",
    "email":"manomaster@163.com",
    "password":"admin123"
}
```

#### Register Response

```js
200 OK
```

```json
{
    "id":"aksdia-ajdina-doansda-nasdonas123",
    "firstName":"Dylan",
    "lastName":"Chen",
    "email":"manomaster@163.com",
    "token":"sdas..asdaodsa"
}
```