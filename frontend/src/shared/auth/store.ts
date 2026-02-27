import {create} from "zustand";
import type {IdentitySelf} from "@/shared/identity/types";
import {getIdentitySelf} from "@/shared/identity/api";

type AuthState = {
    self: IdentitySelf | null;
    loading: boolean;
    loadSelf: () => Promise<void>;
    setSelf: (self: IdentitySelf | null) => void;
    logout: () => void;
};

export const useAuthStore = create<AuthState>((set) => ({
    self: null,
    loading: true,

    setSelf: (self) => set({self}),
    logout: () => set({self: null}),

    loadSelf: async () => {
        set({loading: true});
        const self = await getIdentitySelf();
        set({self, loading: false});
    },
}));