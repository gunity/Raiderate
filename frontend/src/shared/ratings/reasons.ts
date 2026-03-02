export const ratingReasonLabels: Record<string, string> = {
    revived: "Revived / Saved",
    cover_fire: "Helped with cover fire",
    shared_loot: "Shared loot",
    fair_play: "Fair play",

    insults: "Insults / Toxic",
    attempted_kill: "Tried to kill",
    betrayal_kill: "Betrayal / Kill",
};

export function getRatingReasonLabel(code: string): string {
    return ratingReasonLabels[code] ?? code;
}