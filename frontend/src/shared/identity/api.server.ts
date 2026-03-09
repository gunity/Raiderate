import "server-only";

import { IdentitySelf } from "@/shared/identity/types";
import { env } from "@/shared/env";
import { headers } from "next/headers";

export async function getIdentitySelfServer(): Promise<IdentitySelf | null> {
  const response = await fetch(`${env.gatewayInternalUrl}/api/identity/self`, {
    method: "GET",
    cache: "no-store",
    credentials: "include",
    headers: {
      cookie: (await headers()).get("cookie") ?? "",
      "content-type": "application/json",
    },
  });

  if (!response.ok) {
    return null;
  }

  const result = (await response.json()) as IdentitySelf;
  return result;
}
