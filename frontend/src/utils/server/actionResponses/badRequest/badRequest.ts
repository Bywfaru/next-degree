import type { ActionResponse } from '@/types/actions';

export const badRequest = (error: string): ActionResponse<never> => ({
  error,
  success: false,
});