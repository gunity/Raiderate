# Raiderate

Player reputation platform for ARC Raiders. Players vote on each other using structured reasons; votes aggregate into leaderboard ratings.

---

## What It Does

- Players register and log in with JWT-based authentication
- Users vote on other players using predefined rating reasons (e.g., "Excellent Teamwork", "Toxic Behavior")
- Each reason carries a point delta that adjusts the target player's rating
- Votes can include an optional comment
- A leaderboard ranks players by rating (top and bottom)
- Admins manage rating reasons via an admin panel
- One vote per user per player (enforced at DB level)

---

## Tech Stack

### Frontend
- **Next.js 16** (App Router) + **React 19** + **TypeScript**
- **Tailwind CSS 4**
- **Zustand 5** for auth state
- **React Compiler** (`babel-plugin-react-compiler`) for auto-memoization
- **Lucide React** for icons

### Backend
- **ASP.NET Core 10** (.NET 10)
- **YARP** reverse proxy (gateway)
- **MediatR** (CQRS pattern)
- **EF Core** + **PostgreSQL** (three separate databases)
- **gRPC** for internal service-to-service communication
- **RabbitMQ 4** for async event propagation
- **FluentValidation** for command/query validation
- **JWT** authentication (access + refresh tokens)

---

## Architecture

```
Browser
  → /bff/:path*  (Next.js rewrite)
  → Gateway (YARP)  :${BACKEND_GATEWAY_PORT}
  → backend.identity | backend.players | backend.ratings  (:3000 internally)
```

**Services:**

| Service | Responsibility | DB |
|---------|---------------|-----|
| `Backend.Gateway.Api` | YARP reverse proxy, JWT propagation | — |
| `Backend.Identity.*` | Register, login, JWT auth | `postgres.identity` |
| `Backend.Players.*` | Player profiles, leaderboard | `postgres.players` |
| `Backend.Ratings.*` | Votes, rating reasons | `postgres.ratings` |

**Inter-service communication:**
- **gRPC** — Ratings → Identity & Players on port 5001
- **RabbitMQ** — Ratings publishes `VoteCreated`; Players consumes it and updates ratings

> Vote creation is eventually consistent. `POST /votes` returns before the player's rating is updated.

---

## Project Structure

```
Raiderate/
├── Backend.Gateway.Api/          # YARP reverse proxy
├── Backend.Gateway.Infrastructure/
├── Backend.Identity.*/           # Auth service (Api, Application, Domain, Infrastructure)
├── Backend.Players.*/            # Players & leaderboard
├── Backend.Ratings.*/            # Votes & rating reasons
├── Backend.Shared/               # JWT helpers, middleware, exceptions
├── Backend.Contracts/            # RabbitMQ event contracts
├── Backend.Contracts.Grpc/       # gRPC .proto definitions
├── frontend/                     # Next.js app
├── docker-compose.yaml
├── .env.template
└── Raiderate.sln
```

### Frontend (`frontend/src/`)

```
app/                              # Next.js App Router (server components)
  (site)/
    page.tsx                      # Home — SSR leaderboard
    login/page.tsx
    register/page.tsx
    rate/page.tsx
    player/[nickname]/page.tsx
    admin/rating-reasons/page.tsx
pages/                            # "use client" page components
components/                       # Shared UI (Header, Leaderboard, modals)
shared/
  auth/store.ts                   # Zustand auth store
  identity/                       # API + types
  players/                        # API + types
  ratings/                        # API + types
  votes/                          # API + types
  http/                           # Fetch utilities (client & server)
  env.ts
```

---

## API Routes

All routes accessible from the frontend via `/bff/*` (rewrites to Gateway `/api/*`).

```
POST /api/identity/register
POST /api/identity/login
POST /api/identity/logout
POST /api/identity/refresh
GET  /api/identity/self

GET  /api/players/leaderboard?type=Top&limit=5
GET  /api/players/{nickname}/summary

POST /api/votes                       # Requires auth
GET  /api/votes?playerId=123&limit=5

GET  /api/rating-reasons
GET  /api/rating-reasons/admin        # Requires admin
POST /api/rating-reasons/admin        # Requires admin
PUT  /api/rating-reasons/admin/{id}   # Requires admin
```

---

## Running Locally

### Prerequisites

- Docker + Docker Compose
- Node.js 20+ (for frontend dev)

### 1. Configure environment

```bash
cp .env.template .env
```

Fill in all required values in `.env` (see [Environment Variables](#environment-variables)).

### 2. Start the full stack

```bash
docker-compose up
```

This starts:
- 3 PostgreSQL instances (identity, players, ratings)
- RabbitMQ with management UI
- Gateway, Identity, Players, Ratings backend services
- Frontend dev server

The frontend is available at `http://localhost:${FRONTEND_DEV_PORT}` (default `8088`).

### 3. Frontend dev (standalone)

```bash
cd frontend
npm install
npm run dev
```

> Requires `BACKEND_GATEWAY_URL` set in the environment or via Docker Compose.

---

## Frontend Commands

```bash
cd frontend
npm run dev       # Start dev server (webpack)
npm run build     # Production build
npm run lint      # ESLint
npm run format    # Prettier
```

---

## Environment Variables

Copy `.env.template` to `.env` and fill in all values.

| Variable | Description |
|----------|-------------|
| `BACKEND_GATEWAY_IMAGE` | Docker image for gateway |
| `BACKEND_IDENTITY_IMAGE` | Docker image for identity service |
| `BACKEND_PLAYERS_IMAGE` | Docker image for players service |
| `BACKEND_RATINGS_IMAGE` | Docker image for ratings service |
| `POSTGRES_IDENTITY_USER/PASSWORD/DATABASE` | Identity DB credentials |
| `POSTGRES_PLAYERS_USER/PASSWORD/DATABASE` | Players DB credentials |
| `POSTGRES_RATINGS_USER/PASSWORD/DATABASE` | Ratings DB credentials |
| `RABBIT_MQ_USER` | RabbitMQ username |
| `RABBIT_MQ_PASSWORD` | RabbitMQ password |
| `JWT_KEY` | Secret key for JWT signing |
| `JWT_ISSUER` / `JWT_AUDIENCE` | JWT issuer/audience |
| `JWT_ACCESS_TOKEN_NAME` | Cookie name for access token |
| `JWT_REFRESH_TOKEN_NAME` | Cookie name for refresh token |
| `JWT_ACCESS_TOKEN_EXPIRE_MINUTES` | Access token TTL |
| `JWT_REFRESH_TOKEN_EXPIRE_DAYS` | Refresh token TTL |
| `ADMIN_LOGIN` / `ADMIN_PASSWORD` | Seeded admin credentials |
| `BACKEND_GATEWAY_PORT` | Exposed gateway port (e.g. `8080`) |
| `FRONTEND_DEV_PORT` | Frontend dev server port (e.g. `8088`) |
| `RABBIT_MQ_WEB_PORT` | RabbitMQ management UI port (e.g. `9000`) |

The admin user is automatically seeded by the Identity service on startup using `ADMIN_LOGIN` / `ADMIN_PASSWORD`.

---

## Data Model

**Users** — login, password hash, role (`user` | `admin`)

**Players** — nickname (unique), rating, votes count

**RatingReasons** — code, point value, active flag

**Votes** — player, voter, reason, optional comment, timestamp; unique per (player, voter)
