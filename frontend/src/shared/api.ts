export type ErrorResponse = { message: string };

export const isErrorResponse = (x: unknown) : x is ErrorResponse =>
    typeof x === "object" && x !== null && "message" in x;