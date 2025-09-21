import React, { useState, useEffect } from 'react';
import { getAuctions } from '../services/api';
import AuctionCard from '../components/AuctionCard';
import './HomePage.css';

const HomePage = () => {
  const [auctions, setAuctions] = useState([]);
  const [loading, setLoading] = useState(true);
  const [filter, setFilter] = useState('active'); // 'active', 'upcoming', 'ended'

  useEffect(() => {
    const fetchAuctions = async () => {
      try {
        const data = await getAuctions();
        console.log('Datos recibidos de la API:', data);
        // Agregar esta línea para ver el formato de las fechas
        if (data && data.length > 0) {
          console.log('Formato de fechas:', {
            startDate: data[0].startDate,
            endDate: data[0].endDate,
            currentDate: new Date()
          });
        }
        setAuctions(data);
        setLoading(false);
      } catch (error) {
        console.error('Error fetching auctions:', error);
        setLoading(false);
      }
    };

    fetchAuctions();
  }, []);

  const filterAuctions = () => {
    // Temporalmente devolver todas las subastas sin filtrar
    console.log('Todas las subastas sin filtrar:', auctions);
    return auctions;
    
    // El resto del código comentado
    /*
    const now = new Date();
    console.log('Fecha actual para comparación:', now);
    
    try {
      switch (filter) {
        case 'active':
          return auctions.filter(auction => {
            const startDate = new Date(auction.startDate);
            const endDate = new Date(auction.endDate);
            console.log(`Subasta ${auction.id} - startDate: ${startDate}, endDate: ${endDate}`);
            return startDate <= now && endDate > now;
          });
        case 'upcoming':
          return auctions.filter(auction => new Date(auction.startDate) > now);
        case 'ended':
          return auctions.filter(auction => new Date(auction.endDate) < now);
        default:
          return auctions;
      }
    } catch (error) {
      console.error('Error al filtrar subastas:', error);
      return auctions; // En caso de error, mostrar todas las subastas
    }
    */
  } // Sin punto y coma aquí

  const filteredAuctions = filterAuctions();

  return (
    <div className="home-page">
      <div className="hero-section">
        <h1>Subastas Online</h1>
        <p>Encuentra artículos únicos y haz tus ofertas</p>
      </div>
      
      <div className="filter-section">
        <button 
          className={`filter-btn ${filter === 'active' ? 'active' : ''}`}
          onClick={() => setFilter('active')}
        >
          Subastas Activas
        </button>
        <button 
          className={`filter-btn ${filter === 'upcoming' ? 'active' : ''}`}
          onClick={() => setFilter('upcoming')}
        >
          Próximas Subastas
        </button>
        <button 
          className={`filter-btn ${filter === 'ended' ? 'active' : ''}`}
          onClick={() => setFilter('ended')}
        >
          Subastas Finalizadas
        </button>
      </div>

      {loading ? (
        <div className="loading">Cargando subastas...</div>
      ) : filteredAuctions.length > 0 ? (
        <div className="auctions-grid">
          {filteredAuctions.map(auction => (
            <div key={auction.id} className="auction-item">
              <AuctionCard auction={auction} />
            </div>
          ))}
        </div>
      ) : (
        <div className="no-auctions">
          No hay subastas disponibles en esta categoría.
        </div>
      )}
    </div>
  );
};

export default HomePage;