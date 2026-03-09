export default async function apiFetch(
  input: string | URL | Request,
  init?: RequestInit,
): Promise<Response> {
  const first = await fetch(input, {
    ...init,
    cache: "no-store",
    credentials: "include",
    headers: { "content-type": "application/json" },
  });

  if (first.status !== 401) {
    return first;
  }

  const refreshed = await fetch(`/bff/identity/refresh`, {
    method: "POST",
    credentials: "include",
  });

  if (!refreshed.ok) {
    return first;
  }

  return await fetch(input, {
    ...init,
    credentials: "include",
  });
}
