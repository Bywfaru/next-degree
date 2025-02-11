import type { ActionResponse } from '@/types/actions';

export const ok = <T>(data: T): ActionResponse<T> => ({
  data,
  success: true,
});