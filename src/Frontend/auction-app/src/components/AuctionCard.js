import React from 'react';
import { Link } from 'react-router-dom';
import './AuctionCard.css';

const AuctionCard = ({ auction }) => {
  const isEnded = new Date(auction.endDate) < new Date();
  
  return (
    <div className="auction-card">
      <img 
        src={auction.imageUrl || 'https://via.placeholder.com/300x200?text=No+Image'} 
        alt={auction.title} 
        className="auction-image"
      />
      <div className="auction-content">
        <h3>{auction.title}</h3>
        <p className="auction-description">{auction.description}</p>
        <p className="auction-price">Precio inicial: ${auction.startingPrice}</p>
        <p className="auction-date">
          {isEnded ? 'Finalizada' : `Finaliza: ${new Date(auction.endDate).toLocaleDateString()}`}
        </p>
        <Link to={`/auctions/${auction.id}`} className="btn-view-auction">
          Ver detalles
        </Link>
      </div>
    </div>
  );
};

export default AuctionCard;