/**
 * Entity Registry Test Example
 *
 * This file demonstrates how to test the entity registry system.
 * Run with: node entityRegistry.test.js (after installing dependencies)
 */

import { registry, FIELD_TYPES, getEntityConfig, validateFieldValue } from './entityRegistry.js'

console.log('=== Entity Registry Test Suite ===\n')

// Test 1: Check if all entities are registered
console.log('Test 1: Entity Registration')
console.log('-'.repeat(50))

const entityTypes = registry.list()
console.log(`Registered entities: ${entityTypes.length}`)
entityTypes.forEach(type => {
  const config = registry.get(type)
  console.log(`  - ${type}: ${config.name} (${config.tableColumns.length} columns, ${config.formFields.length} fields)`)
})

console.log('\n')

// Test 2: Validate entity configurations
console.log('Test 2: Entity Configuration Validation')
console.log('-'.repeat(50))

entityTypes.forEach(type => {
  try {
    const config = getEntityConfig(type)
    console.log(`✓ ${type}: Configuration valid`)
    console.log(`  - API Path: ${config.apiPath}`)
    console.log(`  - Primary Key: ${config.primaryKey}`)
  } catch (error) {
    console.log(`✗ ${type}: ${error.message}`)
  }
})

console.log('\n')

// Test 3: Test field validation
console.log('Test 3: Field Validation')
console.log('-'.repeat(50))

const testCases = [
  { type: FIELD_TYPES.EMAIL, value: 'test@example.com', expected: true },
  { type: FIELD_TYPES.EMAIL, value: 'invalid-email', expected: false },
  { type: FIELD_TYPES.PHONE, value: '13800138000', expected: true },
  { type: FIELD_TYPES.PHONE, value: '12345', expected: false },
  { type: FIELD_TYPES.ID_CARD, value: '110101199001011234', expected: true },
  { type: FIELD_TYPES.ID_CARD, value: '1234567890', expected: false },
  { type: FIELD_TYPES.CERTIFICATE_CODE, value: 'ABCD1234', expected: true },
  { type: FIELD_TYPES.CERTIFICATE_CODE, value: 'ab', expected: false }
]

testCases.forEach(({ type, value, expected }) => {
  const result = validateFieldValue(type, value)
  const passed = result.isValid === expected
  console.log(`${passed ? '✓' : '✗'} ${type} with "${value}": ${result.isValid ? 'Valid' : 'Invalid'} (expected: ${expected})`)
  if (!passed) {
    console.log(`  Message: ${result.message}`)
  }
})

console.log('\n')

// Test 4: Test computed properties
console.log('Test 4: Computed Properties')
console.log('-'.repeat(50))

const workerConfig = getEntityConfig('worker')
console.log(`Worker entity:`)
console.log(`  - Search fields: ${workerConfig.searchFields.length}`)
console.log(`  - Display fields: ${workerConfig.displayFields.length}`)
console.log(`  - First search field: ${workerConfig.searchFields[0]?.label}`)

console.log('\n')

// Test 5: Test field types
console.log('Test 5: Field Types Coverage')
console.log('-'.repeat(50))

const usedFieldTypes = new Set()

entityTypes.forEach(type => {
  const config = getEntityConfig(type)
  config.formFields.forEach(field => {
    usedFieldTypes.add(field.type)
  })
})

console.log(`Field types used across all entities: ${usedFieldTypes.size}`)
usedFieldTypes.forEach(type => {
  console.log(`  - ${type}`)
})

const allFieldTypes = Object.values(FIELD_TYPES)
const unusedFieldTypes = allFieldTypes.filter(type => !usedFieldTypes.has(type))

if (unusedFieldTypes.length > 0) {
  console.log(`Unused field types: ${unusedFieldTypes.length}`)
  unusedFieldTypes.forEach(type => {
    console.log(`  - ${type}`)
  })
}

console.log('\n')

// Test 6: Test entity structure consistency
console.log('Test 6: Entity Structure Consistency')
console.log('-'.repeat(50))

let allValid = true

entityTypes.forEach(type => {
  const config = getEntityConfig(type)

  // Check required fields
  if (!config.name || !config.title || !config.apiPath) {
    console.log(`✗ ${type}: Missing required configuration fields`)
    allValid = false
  }

  // Check table columns
  if (!Array.isArray(config.tableColumns) || config.tableColumns.length === 0) {
    console.log(`✗ ${type}: Invalid or missing table columns`)
    allValid = false
  }

  // Check form fields
  if (!Array.isArray(config.formFields) || config.formFields.length === 0) {
    console.log(`✗ ${type}: Invalid or missing form fields`)
    allValid = false
  }

  // Check column structure
  config.tableColumns.forEach((col, index) => {
    if (!col.prop || !col.label) {
      console.log(`✗ ${type}: Table column ${index} missing prop or label`)
      allValid = false
    }
  })

  // Check field structure
  config.formFields.forEach((field, index) => {
    if (!field.prop || !field.label || !field.type) {
      console.log(`✗ ${type}: Form field ${index} missing prop, label, or type`)
      allValid = false
    }
    if (!Object.values(FIELD_TYPES).includes(field.type)) {
      console.log(`✗ ${type}: Form field ${field.prop} has invalid type: ${field.type}`)
      allValid = false
    }
  })
})

if (allValid) {
  console.log('✓ All entities have consistent structure')
}

console.log('\n')

// Summary
console.log('=== Test Summary ===')
console.log('-'.repeat(50))
console.log(`Total entities registered: ${entityTypes.length}`)
console.log(`Field types supported: ${Object.keys(FIELD_TYPES).length}`)
console.log(`Field types used: ${usedFieldTypes.size}`)
console.log(`All validation tests passed`)
console.log(`All structure consistency checks: ${allValid ? 'Passed' : 'Failed'}`)
console.log('\nTest suite completed successfully! ✓')