import React, { useState, useEffect } from 'react';
import { getUserAuctions, createAuction } from '../services/api';
import AuctionCard from '../components/AuctionCard';
import './ProfilePage.css';

const ProfilePage = () => {
  const [userAuctions, setUserAuctions] = useState([]);
  const [loading, setLoading] = useState(true);
  const [showForm, setShowForm] = useState(false);
  const [newAuction, setNewAuction] = useState({
    title: '',
    description: '',
    imageUrl: '',
    startingPrice: 0,
    startDate: '',
    endDate: '',
    sellerId: 1 // Usuario de ejemplo
  });

  useEffect(() => {
    const fetchUserAuctions = async () => {
      try {
        const data = await getUserAuctions();
        setUserAuctions(data);
        setLoading(false);
      } catch (error) {
        console.error('Error fetching user auctions:', error);
        setLoading(false);
      }
    };

    fetchUserAuctions();
  }, []);

  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setNewAuction({
      ...newAuction,
      [name]: name === 'startingPrice' ? parseFloat(value) : value
    });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      await createAuction(newAuction);
      // Recargar las subastas del usuario
      const data = await getUserAuctions();
      setUserAuctions(data);
      // Resetear el formulario
      setNewAuction({
        title: '',
        description: '',
        imageUrl: '',
        startingPrice: 0,
        startDate: '',
        endDate: '',
        sellerId: 1
      });
      setShowForm(false);
    } catch (error) {
      console.error('Error creating auction:', error);
    }
  };

  return (
    <div className="profile-page">
      <div className="profile-header">
        <h1>Mi Perfil</h1>
        <button 
          className="btn btn-primary" 
          onClick={() => setShowForm(!showForm)}
        >
          {showForm ? 'Cancelar' : 'Crear Nueva Subasta'}
        </button>
      </div>

      {showForm && (
        <div className="auction-form-container">
          <h2>Crear Nueva Subasta</h2>
          <form onSubmit={handleSubmit} className="auction-form">
            <div className="form-group">
              <label htmlFor="title">Título</label>
              <input
                type="text"
                className="form-control"
                id="title"
                name="title"
                value={newAuction.title}
                onChange={handleInputChange}
                required
              />
            </div>
            <div className="form-group">
              <label htmlFor="description">Descripción</label>
              <textarea
                className="form-control"
                id="description"
                name="description"
                value={newAuction.description}
                onChange={handleInputChange}
                required
              />
            </div>
            <div className="form-group">
              <label htmlFor="imageUrl">URL de la imagen</label>
              <input
                type="text"
                className="form-control"
                id="imageUrl"
                name="imageUrl"
                value={newAuction.imageUrl}
                onChange={handleInputChange}
              />
            </div>
            <div className="form-group">
              <label htmlFor="startingPrice">Precio inicial</label>
              <input
                type="number"
                className="form-control"
                id="startingPrice"
                name="startingPrice"
                value={newAuction.startingPrice}
                onChange={handleInputChange}
                min="0"
                step="0.01"
                required
              />
            </div>
            <div className="form-group">
              <label htmlFor="startDate">Fecha de inicio</label>
              <input
                type="datetime-local"
                className="form-control"
                id="startDate"
                name="startDate"
                value={newAuction.startDate}
                onChange={handleInputChange}
                required
              />
            </div>
            <div className="form-group">
              <label htmlFor="endDate">Fecha de finalización</label>
              <input
                type="datetime-local"
                className="form-control"
                id="endDate"
                name="endDate"
                value={newAuction.endDate}
                onChange={handleInputChange}
                required
              />
            </div>
            <button type="submit" className="btn btn-success">Crear Subasta</button>
          </form>
        </div>
      )}

      <div className="user-auctions">
        <h2>Mis Subastas</h2>
        {loading ? (
          <p>Cargando subastas...</p>
        ) : userAuctions.length > 0 ? (
          <div className="auctions-grid">
            {userAuctions.map(auction => (
              <AuctionCard key={auction.id} auction={auction} />
            ))}
          </div>
        ) : (
          <p>No tienes subastas activas. ¡Crea una nueva subasta!</p>
        )}
      </div>
    </div>
  );
};

export default ProfilePage;