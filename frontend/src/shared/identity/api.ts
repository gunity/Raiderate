"use client";

import { IdentitySelf } from "@/shared/identity/types";
import apiFetch from "@/shared/http/fetch";

export async function getIdentitySelf(): Promise<IdentitySelf | null> {
  const response = await apiFetch(`/bff/identity/self`, {
    method: "GET",
  });

  if (!response.ok) {
    return null;
  }

  const result = (await response.json()) as IdentitySelf;
  return result;
}

export async function identityLogout(): Promise<void> {
  const response = await apiFetch(`/bff/identity/logout`, {
    method: "POST",
  });

  return;
}
