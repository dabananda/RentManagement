\# RentManagement API Documentation (v1)



\*Last updated: 2025‑10‑05\*



This document describes the REST API exposed by \*\*RentManagement.Api\*\*. It’s written for frontend engineers building the Angular client (or any HTTP client).



---



\## 1) Base URL \& Versioning



\* \*\*Base URL (dev):\*\* `https://localhost:5001` (HTTPS is enforced by default)

\* \*\*Prefix:\*\* All endpoints are under `/api/\*` (e.g., `/api/Shop`).

\* \*\*Versioning:\*\* Not implemented yet. Treat current surface as v1.



> \*\*CORS:\*\* The API allows requests from `http://localhost:4200` (Angular dev server).



---



\## 2) Authentication \& Authorization



The API uses \*\*JWT Bearer\*\* tokens (ASP.NET Core Identity) with email‑confirmed users.



\### Flow



1\. \*\*Register\*\* → user receives \*\*email confirmation\*\* link.

2\. \*\*Confirm Email\*\* → enables login.

3\. \*\*Login\*\* → returns `token` (JWT) + `roles`.

4\. \*\*Use token\*\* → include `Authorization: Bearer <token>` in subsequent requests.



\### Roles



\* `Admin`, `Owner`. New sign‑ups are automatically given `Owner`.

\* Current controllers use `\[Authorize]` (no per‑action role policy enforced today).



\### Token Lifetime



\* Access token TTL: \*\*7 days\*\*.



\### Required Headers (after login)



```http

Authorization: Bearer <JWT>

Content-Type: application/json

```



---



\## 3) Error Model \& Status Codes



\* Validation failures return \*\*400\*\* with standard ASP.NET validation payload.

\* Business rule failures often return \*\*400\*\* with a JSON shape `{ "message": "..." }`.

\* Not found resources return \*\*404\*\*.

\* Successful create returns \*\*201 Created\*\* with `Location` header.

\* Successful delete/update commonly return \*\*204 No Content\*\*.



---



\## 4) Data Model (DTOs)



Below are DTOs consumed/returned by the API. Types reflect server models.



\### Auth



\*\*LoginResponseDto\*\*



```ts

{

&nbsp; email: string,

&nbsp; token: string,   // JWT

&nbsp; roles: string\[]

}

```



\### Shop



\*\*ShopCreateDto (request)\*\*



```ts

{

&nbsp; shopNumber: string,           // required, max 10

&nbsp; floor: string,                // required

&nbsp; areaSqFt: number              // 10..10000

}

```



\*\*ShopListDto (response list/detail)\*\*



```ts

{

&nbsp; id: number,

&nbsp; shopNumber: string,

&nbsp; areaSqFt: number,

&nbsp; floor: string,

&nbsp; isOccupied: boolean,

&nbsp; agreements: AgreementWithTenantDto\[]

}

```



\*\*ShopDto (simple response)\*\*



```ts

{ id: number, shopNumber: string, areaSqFt: number, floor: string, isOccupied: boolean }

```



\### Tenant



\*\*TenantCreateDto (request)\*\*



```ts

{ name: string, phone: string, email?: string }

```



\*\*TenantDetailsDto (response)\*\*



```ts

{ id: number, name: string, phone: string, email: string }

```



\*\*TenantDto (response with agreements)\*\*



```ts

{

&nbsp; id: number,

&nbsp; name: string,

&nbsp; phone: string,

&nbsp; email: string,

&nbsp; agreements: AgreementWithShopDto\[]

}

```



\### Rental Agreement



\*\*RentalAgreementCreateDto (request)\*\*



```ts

{

&nbsp; rentAmount: number,           // required

&nbsp; securityFee: number,          // optional (0 = none)

&nbsp; startDate: string,            // DateOnly ISO (YYYY-MM-DD)

&nbsp; endDate?: string,             // DateOnly ISO

&nbsp; shopId: number,               // required

&nbsp; tenantId: number              // required

}

```



\*\*AgreementDetailsDto (response list)\*\* and \*\*RentalAgreementDto (single)\*\*



```ts

{

&nbsp; id: number,

&nbsp; rentAmount: number,

&nbsp; securityFee: number,

&nbsp; startDate: string,            // DateOnly ISO

&nbsp; endDate?: string,             // DateOnly ISO

&nbsp; isActive: boolean,

&nbsp; shop: ShopDetailsDto,

&nbsp; tenant: TenantDetailsDto

}

```



\*\*AgreementWithTenantDto\*\*



```ts

{ id: number, rentAmount: number, securityFee: number, startDate: string, endDate?: string, isActive: boolean, tenantId: number, tenant: TenantDetailsDto }

```



\*\*AgreementWithShopDto\*\*



```ts

{ id: number, rentAmount: number, securityFee: number, startDate: string, endDate?: string, isActive: boolean, shop: ShopDetailsDto }

```



\### Rent Record



\*\*RentRecordCreateDto (request)\*\*



```ts

{

&nbsp; shopId: number,               // required

&nbsp; year?: number,                // default: current year

&nbsp; month?: number,               // 1..12, default: current month

&nbsp; amount?: number,              // default: rentAmount from active agreement

&nbsp; notes?: string

}

```



\*\*RentRecordDto (response)\*\*



```ts

{

&nbsp; id: number,

&nbsp; year: number,

&nbsp; month: number,

&nbsp; amount: number,

&nbsp; paidOn: string,               // DateOnly ISO (created date)

&nbsp; notes?: string,

&nbsp; agreement: AgreementWithTenantDto,

&nbsp; shop: ShopDto

}

```



---



\## 5) Resource Endpoints



All endpoints are JSON. Paths are case‑insensitive.



\### 5.1 Auth



\*\*POST /api/Auth/register\*\*



\* Request: `{ email, password }`

\* Responses:



&nbsp; \* `200 OK` `{ message }` (user created, email sent)

&nbsp; \* `400 Bad Request` `{ message }` (duplicate or validation)



\*\*POST /api/Auth/login\*\*



\* Request: `{ email, password }`

\* Responses:



&nbsp; \* `200 OK` `LoginResponseDto`

&nbsp; \* `401 Unauthorized` (invalid credentials or email not confirmed)



\*\*POST /api/Auth/ConfirmEmail?userId=...\&token=...\*\*



\* Use link from email. Frontend typically calls this endpoint after user clicks Angular route and passes `userId` + `token`.

\* Responses: `200 OK` `{ message }` or `400 Bad Request`.



\*\*GET /api/Auth/TestAuth\*\* \*(requires token)\*



\* Simple probe that echoes user identity \& roles.



\### 5.2 Shop (requires token)



\*\*GET /api/Shop\*\* → `ShopListDto\[]`



\* Returns all shops for \*\*current user\*\* (see Data Scoping).



\*\*GET /api/Shop/{id}\*\* → `ShopListDto`



\* Returns a single shop with current and historical agreements.



\*\*POST /api/Shop\*\*



\* Body: `ShopCreateDto`

\* Responses: `201 Created` (with created shop), `400` on validation.



\*\*PUT /api/Shop/{id}\*\*



\* Body: `ShopCreateDto`

\* Responses: `204 No Content` on success, `404 Not Found` if shop missing \*\*or\*\* if attempting to change `shopNumber` while `isOccupied === true`.



\*\*DELETE /api/Shop/{id}\*\*



\* Responses: `204 No Content` or `404 Not Found` if missing \*\*or\*\* if `isOccupied === true` (deletions are blocked while occupied).



\### 5.3 Tenant (requires token)



\*\*GET /api/Tenant\*\* → `TenantDto\[]`

\*\*GET /api/Tenant/{id}\*\* → `TenantDto`

\*\*POST /api/Tenant\*\* (body: `TenantCreateDto`) → `201 Created`

\*\*PUT /api/Tenant/{id}\*\* (body: `TenantCreateDto`) → `204 No Content` or `404 Not Found`

\*\*DELETE /api/Tenant/{id}\*\* → `204 No Content` or `404 Not Found`



\### 5.4 Rental Agreement (requires token)



\*\*GET /api/RentalAgreement\*\* → `AgreementDetailsDto\[]`

\*\*GET /api/RentalAgreement/{id}\*\* → `RentalAgreementDto`



\*\*POST /api/RentalAgreement\*\*



\* Body: `RentalAgreementCreateDto`

\* Creates an \*\*active\*\* agreement and marks the target `Shop.isOccupied = true`.

\* Responses: `201 Created` with `RentalAgreementDto`, `400` if:



&nbsp; \* `Shop` not found

&nbsp; \* `Tenant` not found

&nbsp; \* Shop already occupied



\*\*POST /api/RentalAgreement/EndAgreement/{id}\*\*



\* Marks agreement inactive (`isActive = false`) and sets `endDate = today`; also flips `Shop.isOccupied = false`.

\* Responses: `204 No Content` or `404 Not Found`.



\*\*PUT /api/RentalAgreement/{id}\*\*



\* Updates amount/dates/links.

\* Responses: `204 No Content` or `404 Not Found`.



\*\*DELETE /api/RentalAgreement/{id}\*\*



\* Allowed \*\*only if inactive\*\*. Active agreements must be ended first.

\* Responses: `204 No Content` or `400 Bad Request` (if active) or `404 Not Found`.



\### 5.5 Rent Record (requires token)



\*\*GET /api/RentRecord\*\* → `RentRecordDto\[]`



\* \*\*Query params (optional):\*\* `shopId`, `tenantId`, `year`, `month`.

\* Sorting: \*\*Year desc, Month desc\*\*.



\*\*GET /api/RentRecord/{id}\*\* → `RentRecordDto`



\*\*POST /api/RentRecord\*\*



\* Body: `RentRecordCreateDto`

\* Server resolves the \*\*active agreement\*\* for the given `shopId` and specified (or current) period:



&nbsp; \* If no active agreement → `400` `{ message: "No active rental agreement found..." }`

&nbsp; \* If a record for `(agreementId, year, month)` already exists → `400` `{ message: "A rent record for this agreement and period already exists." }`

&nbsp; \* `amount` defaults to agreement rent; `paidOn` set to today.

\* Success: `201 Created` with `RentRecordDto`.



\*\*DELETE /api/RentRecord/{id}\*\* → `204 No Content` or `404 Not Found`.



---



\## 6) Data Scoping \& Multi‑Tenancy



The API automatically filters data so users only see their own records. Every entity has `createdByUserId`, and the DbContext applies a \*\*global query filter\*\* for the current user. This means:



\* A user can only list/read/update/delete shops/tenants/agreements/rent records they created.

\* Admins can be granted broader access in the future (policy not implemented yet).



> Implementers should always call endpoints with a valid token; otherwise lists will be empty.



---



\## 7) Examples (cURL)



\### Register \& Confirm



```bash

curl -X POST https://localhost:5001/api/Auth/register \\

&nbsp; -H "Content-Type: application/json" \\

&nbsp; -d '{"email":"owner@example.com","password":"Passw0rd!"}'

\# Check your inbox → click link that lands in Angular route /confirm-email

\# Frontend then calls:

curl -X POST "https://localhost:5001/api/Auth/ConfirmEmail?userId=<id>\&token=<urlEncodedToken>"

```



\### Login



```bash

TOKEN=$(curl -s -X POST https://localhost:5001/api/Auth/login \\

&nbsp; -H "Content-Type: application/json" \\

&nbsp; -d '{"email":"owner@example.com","password":"Passw0rd!"}' | jq -r .token)

```



\### Create Shop



```bash

curl -X POST https://localhost:5001/api/Shop \\

&nbsp; -H "Authorization: Bearer $TOKEN" -H "Content-Type: application/json" \\

&nbsp; -d '{"shopNumber":"A-101","floor":"1st","areaSqFt":320.0}'

```



\### Start an Agreement



```bash

curl -X POST https://localhost:5001/api/RentalAgreement \\

&nbsp; -H "Authorization: Bearer $TOKEN" -H "Content-Type: application/json" \\

&nbsp; -d '{

&nbsp;   "rentAmount": 12000,

&nbsp;   "securityFee": 24000,

&nbsp;   "startDate": "2025-10-01",

&nbsp;   "shopId": 5,

&nbsp;   "tenantId": 12

&nbsp; }'

```



\### Record a Rent Payment (uses active agreement automatically)



```bash

curl -X POST https://localhost:5001/api/RentRecord \\

&nbsp; -H "Authorization: Bearer $TOKEN" -H "Content-Type: application/json" \\

&nbsp; -d '{"shopId":5, "year":2025, "month":10, "amount":12000, "notes":"October rent"}'

```



\### End an Agreement



```bash

curl -X POST https://localhost:5001/api/RentalAgreement/EndAgreement/42 \\

&nbsp; -H "Authorization: Bearer $TOKEN"

```



---



\## 8) Querying, Sorting, Constraints



\* \*\*Sorting:\*\* `RentRecord` list is ordered by (Year desc, Month desc).

\* \*\*Uniqueness:\*\* `(agreementId, year, month)` unique for `RentRecord`.

\* \*\*One active agreement per shop:\*\* enforced via unique index on `(shopId, isActive)` for active ones.

\* \*\*Shop updates:\*\* cannot change `shopNumber` while `isOccupied == true`.

\* \*\*Shop delete:\*\* blocked while `isOccupied == true`.

\* \*\*Agreement delete:\*\* only allowed when `isActive == false`.



---



\## 9) Email \& Environment Setup (FYI for local dev)



Important configuration keys used by the API (usually in `appsettings.Development.json`):



\* `ConnectionStrings:DefaultConnection` (SQL Server)

\* `Jwt:Issuer`, `Jwt:Audience`, `Jwt:Key`

\* `EmailSettings:SenderEmail`, `SenderPassword`, `SmtpServer`, `SmtpPort`

\* `AdminUser:Email`, `AdminUser:Password` (seeded only in Development)



The backend sends the Angular‑facing email confirmation link to:



```

http://localhost:4200/confirm-email?userId=<id>\&token=<token>

```



---



\## 10) OpenAPI / Swagger



In \*\*Development\*\* the app exposes OpenAPI endpoints and maps them automatically. You can discover schemas and try endpoints from a Swagger‑like UI. If not visible, ensure the environment is `Development`.



---



\## 11) Frontend Integration Tips



\* Keep JWT in a secure store; add an `Authorization` header to all protected routes.

\* On login hydration, read stored auth state and set your HTTP interceptor.

\* After creating/updating Shops or RentRecords, re‑fetch lists to reflect server‑side projections (agreements etc.).

\* Prefer IDs from server responses; don’t assume local state equals server after mutations.



---



\## 12) Future Enhancements (suggested)



\* Add \*\*pagination\*\* for list endpoints.

\* Add \*\*role policies\*\* (e.g., Admin‑only destructive ops).

\* Add \*\*PATCH\*\* for partial updates.

\* Expose \*\*Swagger in all environments\*\* (read‑only in production).

\* Standardize error shapes to `{ code, message, details? }`.



---



\*\*That's it!\*\* This doc mirrors the current controllers/services and is safe for immediate frontend use. If you need a formal OpenAPI file (YAML/JSON) for client codegen, ping me and I’ll export one next.



