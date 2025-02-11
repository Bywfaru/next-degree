import type { ZodError } from 'zod';

export const getZodErrorsString = (zodError: ZodError) => {
  const flattenedErrors = zodError.flatten();
  const flattenedFieldErrors = Object.entries(flattenedErrors.fieldErrors)
    .map(([field, errors]) => `${field}: ${(errors ?? []).join(', ')}`);
  const errors = flattenedErrors.formErrors.concat(flattenedFieldErrors);

  return errors.join(', ');
};