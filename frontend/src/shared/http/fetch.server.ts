import { env } from "@/shared/env";
import { cookies } from "next/headers";

export default async function apiFetchServer(
  input: string | URL | Request,
  init?: RequestInit,
): Promise<Response> {
  const cookieStore = await cookies();
  const cookieHeader = cookieStore.toString();

  const buildInit = (extra?: RequestInit): RequestInit => ({
    ...init,
    cache: "no-store",
    credentials: "include",
    headers: {
      "content-type": "application/json",
      ...init?.headers,
      ...extra?.headers,
      cookie: cookieHeader,
    },
  });

  const first = await fetch(input, buildInit());

  if (first.status !== 401) {
    return first;
  }

  const refreshed = await fetch(
    `${env.gatewayInternalUrl}/api/identity/refresh`,
    buildInit({ method: "POST" }),
  );

  if (!refreshed.ok) {
    return first;
  }

  return await fetch(input, buildInit());
}
