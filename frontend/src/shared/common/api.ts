import "server-only";

import { getIdentitySelfServer } from "@/shared/identity/api.server";

export default async function isAdmin(): Promise<boolean> {
  const self = await getIdentitySelfServer();

  if (self === null) {
    return false;
  }

  if (self.role !== "admin") {
    return false;
  }

  return true;
}
