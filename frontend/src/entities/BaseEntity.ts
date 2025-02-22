import { z } from 'zod';

export const BASE_ENTITY_SCHEMA = z.object({
  id: z.string(),
  createdAt: z.coerce.date(),
  updatedAt: z.coerce.date(),
});
