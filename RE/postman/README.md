# RE API – Postman collection and environments

This folder contains a **Postman collection** and **environment files** for testing all RE API endpoints (Project, User, Customer).

## Files

| File | Description |
|------|-------------|
| `RE_API.postman_collection.json` | Collection with all endpoints and test scripts |
| `RE_Local.postman_environment.json` | Local (e.g. `http://localhost:80`) |
| `RE_Dev.postman_environment.json` | Dev server |
| `RE_Production.postman_environment.json` | Production server |

## Setup

1. **Import in Postman**
   - Open Postman → **Import** → drag or select:
     - `RE_API.postman_collection.json`
     - All three `RE_*.postman_environment.json` files

2. **Configure environment**
   - Select an environment (e.g. **RE - Local**) from the top-right dropdown.
   - Edit the environment (eye icon → Edit) and set:
     - **base_url**: your API base URL (e.g. `http://localhost:80` or your IIS port).
     - **api_key**: the value for the `x-api-key` header (must match your API configuration).

3. **Run requests**
   - Run a single request from the collection, or  
   - Use **Collection Runner** to run the whole collection (or a folder) against the selected environment.

## Authentication

All endpoints expect **API Key** in the header:

- **Header name:** `x-api-key`  
- **Value:** set in the environment as `api_key`.

The collection is configured to send this header on every request.

## Test scripts

The collection has **collection-level test scripts** that run after each request:

- Assert that the **status code** is one of `200`, `201`, `202`, `204`, or `500`.
- When the response is JSON and has a `StatusCode` property, assert that **StatusCode** and **Message** exist.

You can add more tests per request in the **Tests** tab of any request.

## Running with Newman (CLI)

To run the collection from the command line (e.g. in CI):

```bash
# Install Newman (one time)
npm install -g newman

# Run collection with an environment
newman run RE_API.postman_collection.json -e RE_Local.postman_environment.json
```

Update `api_key` and `base_url` in the environment file (or pass `--env-var`) before running.
