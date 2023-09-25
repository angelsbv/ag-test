// @ts-check
const { test, expect } = require('@playwright/test');

const TEST_FIRST_NAME = '__<<Test First Name>>__';

const client = {
  firstName: TEST_FIRST_NAME,
  lastName: 'Test Last Name',
  contactNumber: '8095555555',
  occupation: 'Test Dev',
  cards: [
    {
      type: '1',
      bank: 'Bank 1',
      cardNumber: '1234 1234 1234 1234',
      expiryMonth: 9,
      expiryYear: 2026
    },
    {
      type: '2',
      bank: 'Bank 2',
      cardNumber: '1234 1234 1234 1235',
      expiryMonth: 10,
      expiryYear: 2027
    }
  ]
};

const cardTypes = {
  '1': 'Credit',
  '2': 'Debit'
};

test.describe('Testing base functionalities', () => {

  test.beforeEach(async ({ page }) => {
    await page.goto('https://localhost:7023/');
  });

  test('Can delete client if exists', async ({ page }) => {
    const row = page.locator('tr', { has: page.locator(`text="${TEST_FIRST_NAME}"`) }); 
    if (await row.count() === 1) {
      await row.getByRole('link', { name: 'Delete' }).click();
      await page.locator('input[type="submit"][value="Delete"]').click();
    }

    expect(await page.locator('tr', { has: page.locator(`text="${TEST_FIRST_NAME}"`) }).count()).toBe(0);
  });

  test('Can create client with 2 cards', async ({ page }) => {
    await page.locator('[href="/Client/Create"]').click();

    await page.locator('#FirstName').fill(client.firstName);
    await page.locator('#LastName').fill(client.lastName);
    await page.locator('#ContactNumber').fill(client.contactNumber);
    await page.locator('#Occupation').fill(client.occupation);

    for (const card of client.cards) {
      await page.locator('#create-new').click();
      expect(await page.locator('#modal').isVisible());
  
      await page.locator('#CardType').selectOption(card.type);
      await page.locator('#Bank').fill(card.bank);
      await page.locator('#CardNumber').fill(card.cardNumber);
      await page.locator('#ExpiryMonth').fill(card.expiryMonth.toString());
      await page.locator('#ExpiryYear').fill(card.expiryYear.toString());
  
      await page.locator('#btn-save-card').click();
    }

    await page.locator('input[type="submit"][value="Create"]').click();

    const clientRow = page.locator('tr', { has: page.locator(`text="${client.firstName}"`) });
    expect(await clientRow.locator('td:nth-child(1)').innerText()).toBe(client.firstName);
    expect(await clientRow.locator('td:nth-child(2)').innerText()).toBe(client.lastName);
    expect(await clientRow.locator('td:nth-child(3)').innerText()).toBe(client.contactNumber);
    expect(await clientRow.locator('td:nth-child(4)').innerText()).toBe(client.occupation);

    await clientRow.getByRole('link', { name: 'Edit' }).click();

    for (let i = 0; i < client.cards.length; i++) {
      const card = client.cards[i];
      const cardElemSelector = `tbody tr:nth-child(${i + 1})`;
      const expiryMonthText = Intl.DateTimeFormat('en', { month: 'short' }).format(new Date(2009, card.expiryMonth - 1, 10));
      expect(await page.locator(`${cardElemSelector} td:nth-child(1)`).innerText()).toBe(cardTypes[card.type]);
      expect(await page.locator(`${cardElemSelector} td:nth-child(2)`).innerText()).toBe(card.bank);
      expect(await page.locator(`${cardElemSelector} td:nth-child(3)`).innerText()).toBe(card.cardNumber);
      expect(await page.locator(`${cardElemSelector} td:nth-child(4)`).innerText()).toBe(expiryMonthText);
      expect(await page.locator(`${cardElemSelector} td:nth-child(5)`).innerText()).toBe(card.expiryYear.toString());
    }
  });
})

