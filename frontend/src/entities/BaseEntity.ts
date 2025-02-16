import { z, ZodSchema } from 'zod';

export const BaseEntity = z.object({
  id: z.string(),
  createdAt: z.coerce.date(),
  updatedAt: z.coerce.date(),
});

export const baseEntity = (schema: ZodSchema) => BaseEntity.and(schema);
