const express = require('express');
// Helper: find by username or email
function findByUsernameOrEmail(value) {
return players.find(p => p.username === value || p.email === value);
}


// POST /api/player/register
// Body: { username, email, password }
app.post('/api/player/register', (req, res) => {
const { username, email, password } = req.body;
if (!username || !email || !password) {
return res.status(400).json({ error: 'username, email and password are required.' });
}
if (findByUsernameOrEmail(username) || findByUsernameOrEmail(email)) {
return res.status(409).json({ error: 'User with same username or email already exists.' });
}
const newPlayer = {
playerId: Date.now().toString(),
username,
email,
password // NOTE: plaintext for demo only
};
players.push(newPlayer);
return res.status(201).json({ message: 'Player registered', data: newPlayer });
});


// POST /api/player/login
// Body: { usernameOrEmail, password }
app.post('/api/player/login', (req, res) => {
const { usernameOrEmail, password } = req.body;
if (!usernameOrEmail || !password) {
return res.status(400).json({ error: 'usernameOrEmail and password required.' });
}
const player = players.find(p => (p.username === usernameOrEmail || p.email === usernameOrEmail) && p.password === password);
if (!player) return res.status(401).json({ error: 'Invalid credentials.' });
return res.json({ message: 'Login success', data: player });
});


// GET /api/player?playerId=<id> OR GET /api/player (if no query returns all players)
app.get('/api/player', (req, res) => {
const { playerId } = req.query;
if (playerId) {
const p = findById(playerId);
if (!p) return res.status(404).json({ error: 'Player not found' });
return res.json({ data: p });
}
// return all players
return res.json({ data: players });
});


// PUT /api/player
// Body: { playerId, username?, email?, password? }
app.put('/api/player', (req, res) => {
const { playerId, username, email, password } = req.body;
if (!playerId) return res.status(400).json({ error: 'playerId is required in body' });
const p = findById(playerId);
if (!p) return res.status(404).json({ error: 'Player not found' });
if (username) p.username = username;
if (email) p.email = email;
if (password) p.password = password;
return res.json({ message: 'Player updated', data: p });
});


// DELETE /api/delete/:playerId
app.delete('/api/delete/:playerId', (req, res) => {
const { playerId } = req.params;
const idx = players.findIndex(p => p.playerId === playerId);
if (idx === -1) return res.status(404).json({ error: 'Player not found' });
const removed = players.splice(idx,1)[0];
return res.json({ message: 'Player deleted', data: removed });
});


const PORT = process.env.PORT || 5000;
app.listen(PORT, () => console.log(`Mock server running on port ${PORT}`));
